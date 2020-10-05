attach(Credit)

credit_female <- lm(Balance ~ Ethnicity)
summary(credit_female)
par(mfrow=c(2,2))
plot(credit_female)