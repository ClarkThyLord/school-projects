import os


FILE_EXTENSION = 'sad'
TOKENS = (
    (
        
    )
)


def read(file_path):
    data = ''
    with open(file_path, 'r') as file:
        data = file.read()
    return data

def save(file_path, data):
    with open(file_path.replace(FILE_EXTENSION, 'hex'), 'w') as file:
        file.write(data)


def tokenize(source):
    return ''

def compile(source_file_path, hex_file_path=''):
    pass


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
            print('FOUND\nCompiling...')
            compile(source_file_path)
            print('FINISHED')
        else: print('NOT FOUND')
        if input("Press Enter to continue, or enter \"exit\" to quit...\n").lower() == 'exit':
            print('Goodbye ;)')
            print('*************************')
            exit()
        os.system('cls' if os.name == 'nt' else 'clear')
