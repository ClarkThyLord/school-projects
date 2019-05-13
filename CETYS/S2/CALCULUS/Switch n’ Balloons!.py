class player:
    def __init__(self, name, weight, speed):
        self.name = name

        self.weight = weight
        self.speed = speed
    
    def getGroundPoundDisplacement(self):
        return self.weight / 3
    
    def getGroundPoundRate(self):
        return 5 / self.speed
    
    def getPumpAmount(self):
        return self.weight / 4

    def getPumpRate(self):
        return 1 / (self.weight * self.speed)
    
    # v = spw +  (weight / 4) *  (t / (1 / weight * speed))
    # 60 = spw +  (weight / 4) *  (t / (1 / weight * speed))
    def getPumpATime(self):
        current_time = 1
        current_ballon_volume = 60

        while True:
            current_ballon_volume -= self.getPumpAmount() * (current_time / self.getPumpRate())
            current_time += 1

            if current_ballon_volume <= 0: break

        return current_time
    
    def getPumpBTime(self):
        current_time = 1
        current_ballon_volume = 60

        while True:
            current_ballon_volume -= (1 / 2 * (self.getPumpAmount())) * (current_time / self.getPumpRate()) * ((current_time / self.getPumpRate()) if current_time <= 10 else 1)
            current_time += 1

            if current_ballon_volume <= 0: break

        return current_time

    def toString(self):
        return "Name: {} ~ Weight: {} speed: {} ~ Ground Pound Displacement: {} x {} sec.".format(self.name, round(self.weight), round(self.speed), round(self.getGroundPoundDisplacement()), round(self.getGroundPoundRate()))

class team:
    def __init__(self, switch_player, pump_a_player, pump_b_player):
        self.switch_player = switch_player
        self.pump_a_player = pump_a_player
        self.pump_b_player = pump_b_player
    
    def completionTime():
        return 1

    def best():
        return []

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

# 1 / (2 * v) * t
# 1 / (2 * v) * t^2
# V = vi + 1 / (2 * v) * t^2
# V = vi + 1 / (2 * v) * t + 1 / (2 * v) * t^2
# V = vi + (1 / (2 * v1) + 1 / (2 * v2)) * t
# balloon_max_volume = 60

# def pump_a(player):
    # current_time = 1
    # current_ballon_volume = balloon_max_volume

    # while True:
        # current_ballon_volume -= (current_time / (2 * player.velocity))
# #       current_ballon_volume -= player.getPumpAmount() * (current_time / player.getGroundPoundRate())
        # current_time += 1

        # if current_ballon_volume <= 0: break


    # return current_time
    # # return (balloon_max_volume * player.getPumpRate()) / player.getPumpAmount()

# def pump_b(player):
    # current_time = 1
    # current_ballon_volume = balloon_max_volume

    # while True:
        # current_ballon_volume -= (player.getPumpAmount() * (current_time / player.getPumpRate())) / ((10 - current_time) if current_time < 10 else 1)
        # current_time += 1

        # if current_ballon_volume <= 0: break


    # return current_time

for player in players:
    print(player.name, " ~ Pump A: ", player.getPumpATime(),"sec. | Pump B: ", player.getPumpBTime(), "sec.")

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
