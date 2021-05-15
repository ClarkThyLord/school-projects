import math
import pandas as pd
import seaborn as sns
from wordcloud import WordCloud
import matplotlib.pyplot as plt

# Load quotes.csv as a DataFrame
df = pd.read_csv('data/sd_paving_datasd_v1.csv')
df.dropna()

# Print a preview of the DataFrame
print(df.head())
print("Entries: {len}".format(len=len(df)))
print("Types of Street: {len}".format(len=df["type"].unique()))

sns.countplot(x=df["type"], data=df)
plt.show()


# Material Cost
material_cost = {
    "Concrete": 6.0,
    "Overlay": 1.5,
    "Slurry": 0.6,
}

prices_df = pd.DataFrame({
    "Concrete": 0.0,
    "Overlay": 0.0,
    "Slurry": 0.0,
}, index=["Cost"])

for index, row in df.iterrows():
    sqr_feet = (row["length"] * row["width"])
    if not math.isnan(sqr_feet):
        prices_df[row["type"]] += material_cost[row["type"]] * sqr_feet

print(prices_df.head())

sns.histplot(data=prices_df, x=prices_df.head(), y="Cost")
plt.show()


# Compile all the tags in the DataFrame into a string separated by space
types = ""
for t in df["type"]:
    types += " {t}".format(t=t)

# Generate a word cloud image
wordcloud = WordCloud().generate(types)

# Display the generated image:
plt.imshow(wordcloud, interpolation='bilinear')
plt.axis("off")
plt.show()
