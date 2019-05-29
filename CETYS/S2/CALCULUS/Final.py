import math

speed = 1
strength = 1
intelligence = 1

print('Allocate skill points:')
skill_points = 10
while skill_points > 0:
    print('1) speed\n2) strength\n3) intelligence')
    ans = input()
    if ans == '1': speed += 1
    elif ans == '2': strength += 1
    elif ans == '3': intelligence += 1
    else: continue
    skill_points -= 1

print('Speed:', speed, 'Strength:', strength, 'Intelligence:', intelligence)

time_increments = 0.01

def level_1(time):
    return (strength * speed) * time

def get_level_1_time():
    time = 0
    current = 0
    distance = 5
    
    while current < distance:
        time += time_increments
        current = level_1(time)

    return time

def level_2(time):
    return math.pow(time, 2) / speed

def get_level_2_time():
    time = 0
    current = 0
    distance = 10
    
    while current < distance:
        time += time_increments
        current = level_2(time)

    return time

def level_3(time):
    return (1 / strength) * time

def get_level_3_time():
    time = 0
    current = 0
    distance = 7
    
    while current < distance:
        time += time_increments
        current = level_3(time)

    return time

def level_4(time):
    return intelligence * math.pow(time, 3)

def get_level_4_time():
    time = 0
    current = 0
    distance = 10
    
    while current < distance:
        time += time_increments
        current = level_4(time)

    return time

print(round(get_level_1_time(), 2), 'min.')
print(round(get_level_2_time(), 2), 'min.')
print(round(get_level_3_time(), 2), 'min.')
print(round(get_level_4_time(), 2), 'min.')
