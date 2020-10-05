LoadLibraries=function(){
library(MASS)
install.packages(ISLR)
install.packages(car)
library(ISLR)
library(car)
print("The libraries have been loaded.")
}

#3.6.2
fix(Boston)
names(Boston)

lm.fit = lm(medv~lstat, data=Boston )
attach(Boston)
lm.fit = lm(medv~lstat)
summary(lm.fit)

names(lm.fit)
coef(lm.fit)

#confidence interval for the coefficients estimates ^
confint(lm.fit)

#produce confidence interfvals and prediction intervals for a given value x "lstat"
predict (lm.fit ,data.frame(lstat=c(5 ,10 ,15)),interval = "confidence")
predict (lm.fit ,data.frame(lstat=c(5 ,10 ,15)),interval = "prediction")

plot(lstat ,medv)
abline (lm.fit)

abline (lm.fit ,lwd =3)
abline (lm.fit ,lwd =3, col ="red ")
plot(lstat ,medv ,col ="red ")
plot(lstat ,medv ,pch =20)
plot(lstat ,medv ,pch ="+")
plot (1:20 ,1:20, pch =1:20)

par(mfrow =c(2,2))
plot(lm.fit)

plot(predict (lm.fit), residuals (lm.fit))
plot(predict (lm.fit), rstudent (lm.fit))

#leverage statistics
plot(hatvalues (lm.fit ))
which.max(hatvalues (lm.fit))

#3.6.3
lm.fit = lm(medv~lstat+age ,data=Boston )
summary(lm.fit)

#to access all 13 variables for the Boston data set
lm.fit = lm(medv~.,data=Boston )
summary(lm.fit)

#compute variance inflation factors
vif(lm.fit)

#all variables but age
#lm.fit1 = lm(medv~.-age ,data=Boston )
lm.fit1 = update(lm.fit , ~.-age)
summary(lm.fit1)

#3.6.4
#Interaction terms
summary(lm(medv~lstat*age, data=Boston))

#3.6.5 transformation of predictors
lm.fit2 = lm(medv~lstat +I(lstat ^2))
summary(lm.fit2)

#comparing quadratic and linear fits
lm.fit =lm(medv~lstat)
anova(lm.fit ,lm.fit2)

par(mfrow=c(2,2))
plot(lm.fit2)

#up to the fifth power
lm.fit5=lm(medv~poly(lstat ,5))
summary (lm.fit5)

#log transformation
summary(lm(medv~log(rm),data=Boston))

#3.6.6
fix(Carseats)
names(Carseats)

lm.fit = lm(Sales~.+ Income :Advertising +Price :Age ,data=Carseats)
summary(lm.fit)

#dummy variables 
attach(Carseats)
contrasts(ShelveLoc)

#3.6.7
LoadLibraries()