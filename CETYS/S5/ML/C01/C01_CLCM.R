# 1.1.Imprimir en pantalla la siguiente frase: “Hello,World!”
print("Hello, World!", quote = FALSE)
# 1.2.Crear las variables 𝑥=1 e 𝑦, y evaluar la siguiente función: 𝑦=𝑓(𝑥)=3𝑥^2+2𝑥−6. Despliegue en pantalla el resultado.
x <- 1
y <- 3*x^2+2*x-6
print(y)
# 1.3.Crear el vector 𝑥=[−2,−1,0,1,2]y evaluar la siguiente función: 𝑦=𝑓(𝑥)=3𝑥^2+2𝑥−6. Despliegue en pantalla el resultadoy graficar los resultados obtenidos.
x <- c(-2,-1,0,1,2)
y <- 3*x^2+2*x-6
print(y)
plot(x,y,type = "l")
# 1.4.Crear el vector𝑥=[−2,−1.9,−1.8,...,2], notar que el primer elemento del vector es -2 y el último es 2, con lo cual se deduce que hay una separación de 0.1 entre los elementos del vector. Evaluar la siguiente función: 𝑦=𝑓(𝑥)=3𝑥2+2𝑥−6 y graficar los resultados. Despliegue en pantalla el resultado numérico y gráfico.
x <- seq(-2,2, by=0.1)
y <- 3*x^2+2*x-6
print(y)
plot(x,y,type = "l")
# 1.5.Utilizar el vector 𝑥 creado en la actividad 1.4 y evaluar las siguientes funciones:1)𝑦=sin𝑥,2) 𝑦=log10𝑥,3) 𝑦=𝑒𝑥.Despliegue en pantalla el resultado numérico y gráfico.
x <- seq(-2,2, by=0.1)
func1<-sin(x)
func2<-log10(x)
func3<-exp(x)
plot(x,func1,type = "l")
print(func1)
plot(x,func2,type = "l")
print(func2)
plot(x,func3,type = "l")
print(func3)
# 1.6 Crear los vectores 𝑣1=[1,2,3,4]y 𝑣2=[2,4,6,8]y realizar las siguientes operaciones elemento por elemento: 𝑣1∗𝑣2, 𝑣1/𝑣2, 𝑣1^2, 𝑣1+𝑣2, 𝑣1−𝑣2. Despliegue en pantalla los resultados.
a<-1:4
b<-seq(2,8,by=2)
a%*%b
a/b
a^2
a+b
a-b
# 1.7 Crear las matrices 𝑀= [[4,3,-1], [2,1,0], [0,-4,3], [-1,0,6]] y N= [[-1,2,-2,0], [0,3,1,-1]] y realizar la multiplicación de matrices: 
a<-array(c(4,3,-1,2,1,0,0,-4,3,-1,0,6),dim = c(3,4))
b<-array(c(-1,2,-2,0,0,3,1,-1),dim=c(4,2))
a%*%b
# 1.8.Realizar  un  programa  que  despliegue  una  caja,  un  ovalo,  una  flecha  y  un  diamante  utilizando asteriscos, como se muestra en la figura.
result = ""
for (pos in 0:80) {
  x <- pos %% 9
  y <- pos %/% 9
  if(x == 8){
    result = paste(result, "*\n", sep="")
  } else if (x == 0 || y == 0 || y == 8) {
    result = paste(result, "*", sep="")
  } else {
    result = paste(result, " ", sep="")
  }
}
cat(result)


result = ""
for (pos in 0:80) {
  x <- pos %% 9
  y <- pos %/% 9
  if(((x > 2 && x < 6) && (y == 0 || y == 8)) || (x == 0 && (y > 1 && y < 7)) || ((x == 1 || x == 7) && (y == 1 || y == 7))) {
    result = paste(result, "*", sep="")
  } else if (x == 8 && (y > 1 && y < 7)) {
    result = paste(result, "*\n", sep="")
  } else if (x == 8 && (y < 2 || y > 6)) {
    result = paste(result, "\n", sep="")
  } else {
    result = paste(result, " ", sep="")
  }
}
cat(result)


result = ""
for (posy in 0:8){
  for (posx in 0:4){
    x <- posx %% 5
    y <- posy %% 9
    if (x == 2 || ((y == 1 || y == 2) && (x == 1 || x == 3)) || (y == 2 && x == 0)){
      result = paste(result, "*", sep="")
    } else if (x == 4 && (y < 2 || y > 2)) {
      result = paste(result, "\n", sep="")
    } else if (y == 2 && x == 4){
      result = paste(result, "*\n", sep="")
    } else {
      result = paste(result, " ", sep="")
    }
  }
}
cat(result)

result = ""
for (pos in 0:80) {
  x <- pos %% 9
  y <- pos %/% 9
  if((x == 4 && (y == 0 || y == 8)) || ((y == 1 || y == 7) && (x == 3 || x == 5)) || ((y == 2 || y == 6) && ( x == 2 || x == 6)) || ((y == 3 || y == 5) && (x == 1 || x == 7)) || (y == 4 && x == 0)) {
    result = paste(result, "*", sep="")
  } else if (x == 8 && y == 4) {
    result = paste(result, "*\n", sep="")
  } else if (x == 8 && y != 4) {
    result = paste(result, "\n", sep="")
  } else {
    result = paste(result, " ", sep="")
  }
}
cat(result)

# 1.9. Realizar un programa que despliegue los siguientes patrones utilizando asteriscos.
size = 10
figure <- list()
for (i in 0:size) {
  figure[i] <- paste(paste(replicate(i, "*"), collapse = ""), paste(replicate(size - i, " "), collapse = ""), collapse = "")
}
print(figure)

for (l in figure) {
  print(l, FALSE)
}

for (l in rev(figure)) {
  print(l, FALSE)
}

for (l in figure) {
  print(paste(rev(strsplit(l, split = "")[[1]]), collapse = ""), FALSE)
}

for (l in rev(figure)) {
  print(paste(rev(strsplit(l, split = "")[[1]]), collapse = ""), FALSE)
}

# 1.10. Realizar un programa que despliegue un triángulo utilizando asteriscos. La altura será definida por el usuario, para el ejemplo de la figura la altura es 5.
size<-9 * 2
p=""

for(number in seq(1, size, by=2)){ 
  p = paste(p, paste(replicate(size - number, " "), collapse = ""), paste(replicate(number, "*"), collapse = " "), sep="")
  p = paste(p, "\n")
  
}

cat(p)
