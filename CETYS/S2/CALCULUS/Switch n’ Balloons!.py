class player:
    def __init__(self, name, weight, velocity):
        self.name = name

        self.weight = weight
        self.velocity = velocity
    
    def getGroundPoundDisplacement(self):
        return self.weight / 3
    
    def getGroundPoundRate(self):
        return 5 / self.velocity

    def getPumpRate(self):
        return (self.weight * self.velocity) / 2
    
    def getPumpAmount(self):
        return self.weight / 4

    def toString(self):
        return "Name: {} ~ Weight: {} Velocity: {} ~ Ground Pound Displacement: {} x {} sec.".format(self.name, round(self.weight), round(self.velocity), round(self.getGroundPoundDisplacement()), round(self.getGroundPoundRate()))

players = [
    player("Mario", 3, 3),
    player("Luigi", 5, 2),
    player("Peach", 5, 1.5),
    player("Wario", 2, 5),
    player("Waluigi", 6, 1.5),
    player("Daisy", 3, 4)
]

def get_player(name):
    for player in players:
        if player.name.lower() == name.lower():
            return player


print("---- players ----")
for _player in players:
    print(_player.toString())
print("*****************")

daisy = get_player("daisy")
# ch = ih - (t / gpr)(w / 3)
# 0 = ih - (60 / gpr)(w / 3) -> ih = 0 + (60 / gpr)(w / 3)
# 0 = 48 - (t / gpr)(w / 3) -> t = (48 * gpr) / (w / 3)
switch_height = (60 / daisy.getGroundPoundRate()) * (daisy.getGroundPoundDisplacement())
print("Switch height: {}".format(switch_height))

print("*****************")

print("Player Switch Times:")
for _player in players:
    print(_player.name, " ~ ", round((48 * _player.getGroundPoundRate()) / _player.getGroundPoundDisplacement(), 4))

print("*****************")

# pt = (v * w) / 2
# v+ = w / 4
# va = v+ * (t / pt)
# 60 = v+ * (t / pt) -> t = (60 * pt) / v+
# vb = v+ * t / pt / t
# 60in^2
balloon_max_volume = 60

def pump_a(player):
    current_time = 1
    current_ballon_volume = balloon_max_volume

    while True:
        current_ballon_volume -= player.getPumpAmount() * (current_time / player.getGroundPoundRate())
        current_time += 1

        if current_ballon_volume <= 0: break


    return current_time
    # return (balloon_max_volume * player.getPumpRate()) / player.getPumpAmount()

def pump_b(player):
    current_time = 1
    current_ballon_volume = balloon_max_volume

    while True:
        current_ballon_volume -= (player.getPumpAmount() * (current_time / player.getPumpRate())) / ((10 - current_time) if current_time < 10 else 1)
        current_time += 1

        if current_ballon_volume <= 0: break


    return current_time

for player in players:
    print(player.name, " ~ Pump A: ", pump_a(player),"sec. | Pump B: ", pump_b(player), "sec.")

print("*****************")

playing_players = []
for _ in range(6):
    print("Choose player {}/6...".format(len(playing_players) + 1))
    choosen_player = get_player(input())

    if choosen_player:
        if playing_players.count(choosen_player) == 0:
            playing_players.append(choosen_player)
        else:
            print("Player has already been choosen...")
    else:
        print("Player not found...")
