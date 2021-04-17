import pandas as pd
from wordcloud import WordCloud
import matplotlib.pyplot as plt

# Load quotes.csv as a DataFrame
df = pd.read_csv('output/quotes.csv')

# Print a preview of the DataFrame
print(df.head())

# Compile all the tags in the DataFrame into a string separated by space
words = ""
for tags in df["tags"]:
    for tag in str(tags).split(","):
        words += " {tag}".format(tag=tag)

# Generate a word cloud image
wordcloud = WordCloud().generate(words)

# Display the generated image:
# the matplotlib way:
plt.imshow(wordcloud, interpolation='bilinear')
plt.axis("off")

# lower max_font_size
wordcloud = WordCloud(max_font_size=40).generate(words)
plt.figure()
plt.imshow(wordcloud, interpolation="bilinear")
plt.axis("off")
plt.show()
