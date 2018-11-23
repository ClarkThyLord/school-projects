import json

q_a = []

for i in range(1):
    print('Q:')
    q = input()

    print('A:')
    a = input()

    q_a.append({
        'q': q,
        'a': a
        })

print(json.dumps(q_a))

with open('q_a.json', 'w') as outfile:
    json.dump(q_a, outfile)

print('Saved!')
