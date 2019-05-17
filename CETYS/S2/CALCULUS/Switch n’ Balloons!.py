import math

class Player:
    def __init__(self, name, speed, weight):
        self.name = name
        self.speed = speed
        self.weight = weight

    def switch(self, time, h=0):
        return h + (self.weight / 3) * (time / (5 / self.speed))
    
    def pumpA(self, time, v=0):
        return v + (self.weight / 4) * (time / ((self.speed * self.weight) / 2))
    
    def pumpB(self, time, v=0):
        return math.pow(pumpA(time, v), 2 if time <= 10 else 1)

class Players:
    def __init__(self, roster=[]):
        self.roster = roster

    def getPlayer(self, name):
        for player in self.roster:
            if player.name.lower() == name.lower():
                return player        

    def addPlayer(self, player):
        self.roster.append(player)

    def removePlayer(self, player):
        self.roster.remove(player)

class Team:
    def __init__(self, switch_player, pump_a_player, pump_b_player):
        self.switch_oplayer = switch_player
        self.pump_a_player = pump_a_player
        self.pump_b_player = pump_b_player

players = Players([
    Player('Mario', 3, 3),
    Player('Luigi', 5, 2),
    Player('Peach', 5, 1.5),
    Player('Wario', 2, 5),
    Player('Waluigi', 6, 1.5),
    Player('Daisy', 3, 4)
])

##print(players.getPlayer('Daisy').switch(60))
##print(players.getPlayer('Daisy').pumpA(482))
