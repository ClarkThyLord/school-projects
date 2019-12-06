import os
import binascii


MAX_VALUE = 15
FILE_EXTENSION = 'sad'
TOKENS = {
    'sum': '0',
    'sub': '1',
    'and': '3',
    'or':  '4'
}


def read(file_path):
    data = ''
    with open(file_path, 'r') as file:
        data = file.read()
    return data

def save(file_path, data):
    with open(file_path.replace(FILE_EXTENSION, 'hex'), 'wb') as file:
        file.write(data)


def assembler(source_file_path, dest_file_path=''):
    source = read(source_file_path)
    tokens = source.split()
    if len(tokens) % 2 == 1:
        print('Invalid format')
        return False

    
    command = ''
    commands = b''
    is_token = True
    for token in tokens:
        token = token.lower()
        if token.isalpha() and is_token:
            if token in TOKENS:
                command = TOKENS[token]
            else:
                print('Unrecognized token:', token)
                return False
        elif not token.isnumeric() == is_token and int(token) <= MAX_VALUE:
            commands += binascii.unhexlify(command + token)
        else:
            print('Invalid token:', token)
            return False
        is_token = not is_token
    save(source_file_path if dest_file_path == '' else dest_file_path, commands)
    return True
    


if __name__ == '__main__':
    while True:
        print('************************+')
        print('SAD Compiler v0.0.1     |')
        print('========================+')
        
        print('File to compile: (*.%s)' % FILE_EXTENSION)
        source_file_path = input().replace('"', '').replace("'", '')
        print('File...')
        if not source_file_path.endswith('.' + FILE_EXTENSION):
            print('INVALID')
        elif os.path.exists(source_file_path):
            print('FOUND\nCOMPILING...')
            result = assembler(source_file_path)
            if result: print('SUCCESS')
            else: print('FAILED')
        else: print('NOT FOUND')
        if input("Press Enter to continue, or enter \"exit\" to quit...\n").lower() == 'exit':
            print('Goodbye ;)')
            print('*************************')
            exit()
        os.system('cls' if os.name == 'nt' else 'clear')
