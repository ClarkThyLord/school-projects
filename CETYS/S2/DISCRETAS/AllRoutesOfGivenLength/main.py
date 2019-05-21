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

if __name__ == "__main__":
    file = open('./example.txt', 'r')
    start, end, length, matrix = text_to_input(file.read())
    file.close()

    print('Start: ', start, 'End: ', end, 'Length: ', length, 'Matrix: ', matrix)
