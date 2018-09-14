correct = 0
questions = [
##    {
##        "type": "open",
##        "question": "HELLO WORLD!",
##        "answer": 0,
##        "options": [
##            "A",
##            "B",
##            "C",
##            "D"
##        ]
##    },
##    {
##        "type": "closed",
##        "question": "SOMETHING!",
##        "answer": "hello world!"
##    }
    {
        "type": "closed",
        "question": "A quien le debemos la logica en base a true y false?",
        "answer": 0,
        "options": [
            "George Boole",
            "Charles Darwin",
            "Stalin",
            "Marx"
        ]
    },
    {
        "type": "closed",
        "question": "El padre de las Ciencias Computacionales?",
        "answer": 0,
        "options": [
            "Alan Turing",
            "Jr. Alan Turing",
            "Estib Jobs",
            "Hitler (cuando todavia no era Nazi)"
        ]
    },
    {
        "type": "closed",
        "question": "Colores de hats los Hackers?",
        "answer": 0,
        "options": [
            "Negro, Blanco y Gris",
            "Rojo ,Naranja y Azul Caramelo #43",
            "Todos los colores son color Canela Pasion",
            "Los Hackers no usan sombreros usan fedoras (╯°□°）╯︵ ┻━┻"
        ]
    },
    {
        "type": "closed",
        "question": "Men que le debemos Arquitectura Computacional?",
        "answer": 0,
        "options": [
            "Von Newmann",
            "David Marin Castaneda",
            "Manolo Esparza",
            "Jorge Antonio Mejia Leon"
        ]
    },
    {
        "type": "closed",
        "question": "Donde se encuentra la tecnologia",
        "answer": 0,
        "options": [
            "En todos lados",
            "En mi casa OWO",
            "En la camioneta blanca que dice DULCES GRATIS en el parque Morelos los jueves y domingos en la noche",
            "En Narnia"
        ]
    },
    {
        "type": "closed",
        "question": "Que es una computadora",
        "answer": 0,
        "options": [
            "Dispositivo que recibe y procesa datos que puede ser programable",
            "Mi perro",
            "Un tostador",
            "Todos somos computadoras #staywoke"
        ]
    },
    {
        "type": "closed",
        "question": "Hardware principal de computadora",
        "answer": 0,
        "options": [
            "CPU, DISCO DURO, RAM",
            "Un raton vivo, un mouse y mickey mouse",
            "Un teclado, un piano, y un monitor",
            "La computadora no tiene hardware porque el hardware es un mito"
        ]
    },
    {
        "type": "open",
        "question": "Que dia murio Alan Turing y en donde (La respuesta se pone: numero de dia, mes, año, lugar, dia de la semana",
        "answer": "7, junio, 1954, Wilmslow, Lunes"
    },
    {
        "type": "open",
        "question": "En que años trabajo Alan Turing con Church en la universidad de Princeton (La respuesta se pone: año-año)",
        "answer": "1936-1938"
    },
    {
        "type": "open",
        "question": "En que articulo y apartado dice que el examen extraordinario en CETYS debe de realizarse por escrito (La respuesta se escribe: Numero, letra)",
        "answer": "26, d"
    }
]

print("Trivia del Equipo #1:")

for question in questions:
    print(question["question"])

    if question["type"] == "closed":
        for key, option in enumerate(question["options"]):
            print(str(key) + " - " + option)

        ans = int(input())

        if ans == question["answer"]:
            correct += 1
            print("Correcto!")
        else:
            print("Incorrecto!")
    elif question["type"] == "open":
        ans = input()
        if ans == question["answer"]:
            correct += 1
            print("Correcto!")
        else:
            print("Incorrecto!")

print("Fin!\n", correct, " / ", len(questions))
