attach(Credit)

credit_female <- lm(Balance ~ Gender)
summary(credit_female)
par(mfrow=c(2,2))
plot(credit_female)