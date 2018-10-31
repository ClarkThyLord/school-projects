import os
import base64
import json

data = []

print('Export for WEB? (y, n)')
ans = input()

for file_name in os.listdir('./C1'):
    print('./C1/' + file_name)
    with open('./C1/' + file_name, 'rb') as image_file:
        encoded_string = base64.b64encode(image_file.read())

        data.append({
            'img': ('data:image/jpeg;base64,' if ans == 'y' else '') + encoded_string.decode(),
            'name': file_name.split('.')[0]
        })

with open('./data.json', 'w') as out_file:
    json.dump(data, out_file)
