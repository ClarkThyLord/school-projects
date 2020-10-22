library(ISLR)
Def = glm(default~student, data=Default, family=binomial)
summary(Def)$coef

Def = glm(default~., data=Default, family=binomial)
summary(Def)$coef