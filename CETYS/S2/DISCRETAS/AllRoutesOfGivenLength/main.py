import math

def text_to_input(text):
    num = ''
    nums = []
    for char in text:
        if char.isdigit(): num += char
        elif num == '' and char == '-': num += char
        elif num != '':
            nums.append(int(num))
            num = ''

    if num != '':
            nums.append(int(num))
            num = ''

    return nums[0], nums[1], nums[2], nums[3:]

def pretty_matrix(matrix):
    pretty = ''
    matrix_size = int(math.sqrt(len(matrix)))

    for index in range(len(matrix)):
        pretty += str(matrix[index])
        if index % matrix_size + 1 != matrix_size:  pretty += ' '
        if index % matrix_size + 1 == matrix_size and index != len(matrix) - 1:  pretty += '\n'
    
    return pretty

def matrix_pow(matrix, pow = 2):
    if pow <= 1: return matrix
    
    rows = []
    columns = []
    matrix_size = int(math.sqrt(len(matrix)))
    for dimension in range(matrix_size):
        rows.append([])
        columns.append([])
    
    for index in range(len(matrix)):
        rows[int(index / matrix_size)].append(matrix[index])
        columns[int(index % matrix_size)].append(matrix[index])

    result = []
    for row in rows:
        value = 0
        for column in columns:
            for index in range(len(rows)):
                value += row[index] * column[index]
            result.append(value)
            value = 0

    return matrix_pow(result, pow - 1)

if __name__ == "__main__":
    file = open('./example.txt', 'r')
    start, end, length, matrix = text_to_input(file.read())
    file.close()

    print('Start: ', start, 'End: ', end, 'Length: ', length, 'Matrix: ')
    print(pretty_matrix(matrix))
    print('---')

    result = matrix_pow(matrix, length)

    print('Amount: ', result[start * int(math.sqrt(len(matrix))) + end])
    print(pretty_matrix(result))
    
