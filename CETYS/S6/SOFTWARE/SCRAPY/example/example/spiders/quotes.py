# Scrapy Example
# An open source and collaborative framework for extracting the data you need from websites.
# In a fast, simple, yet extensible way.
#
# Examples of using Scrapy spiders for http://quotes.toscrape.com
# Following: https://docs.scrapy.org/en/latest/intro/tutorial.html
#
# Recommended to create an virtual env.
#
# pip install Scrapy
#
# Create a Scrapy project:
# scrapy startproject tutorial
#
import scrapy


#
# V1
# Spiders are classes that you define and that Scrapy uses to scrape information from a website
# (or a group of websites). They must subclass Spider and define the initial requests to make,
# optionally how to follow links in the pages, and how to parse the downloaded page content to extract data.
#
# Runs from terminal with:
# scrapy crawl quotes
#

class QuotesSpider(scrapy.Spider):
    # Identifies the Spider. It must be unique within a project,
    # that is, you can’t set the same name for different Spiders.
    name = "quotes"

    # Must return an iterable of Requests (you can return a list of
    # requests or write a generator function) which the Spider will begin to crawl from.
    # Subsequent requests will be generated successively from these initial requests.
    def start_requests(self):
        urls = [
            'http://quotes.toscrape.com/page/1/',
            'http://quotes.toscrape.com/page/2/',
        ]
        for url in urls:
            yield scrapy.Request(url=url, callback=self.parse)

    # Method that will be called to handle the response downloaded for each of the requests made.
    # The response parameter is an instance of TextResponse that holds the page content and has
    # further helpful methods to handle it.
    def parse(self, response):
        page = response.url.split("/")[-2]
        filename = f'output/quotes-{page}.html'
        with open(filename, 'wb') as f:
            f.write(response.body)
        self.log(f'Saved file {filename}')


# V2
# Instead of implementing a start_requests() method that generates scrapy.Request objects from URLs,
# you can just define a start_urls class attribute with a list of URLs. This list will then be used
# by the default implementation of start_requests() to create the initial requests for your spider.
#

# class QuotesSpider(scrapy.Spider):
#     name = "quotes"
#     start_urls = [
#         'http://quotes.toscrape.com/page/1/',
#         'http://quotes.toscrape.com/page/2/',
#     ]
#
#     def parse(self, response):
#         page = response.url.split("/")[-2]
#         filename = f'output/quotes-{page}.html'
#         with open(filename, 'wb') as f:
#             f.write(response.body)


# V3
# Until now, it doesn’t extract any data in particular, just saves the whole HTML page to a local file.
# Let’s integrate the extraction logic above into our spider. A Scrapy spider typically generates many
# dictionaries containing the data extracted from the page. To do that, we use the yield Python keyword
# in the callback, as you can see below.
#
# Output can be saved to various formats, such as:
# JSON: scrapy crawl quotes -o output/quotes.json
# CSV:  scrapy crawl quotes -o output/quotes.csv -t csv
#

# class QuotesSpider(scrapy.Spider):
#     name = "quotes"
#     start_urls = [
#         'http://quotes.toscrape.com/page/1/',
#         'http://quotes.toscrape.com/page/2/',
#     ]
#
#     def parse(self, response):
#         for quote in response.css('div.quote'):
#             yield {
#                 'text': quote.css('span.text::text').get(),
#                 'author': quote.css('small.author::text').get(),
#                 'tags': quote.css('div.tags a.tag::text').getall(),
#             }


#
# V4
# Let’s say, instead of just scraping the stuff from the first two pages from the website,
# you want quotes from all the pages in the website. What you see here is Scrapy’s mechanism
# of following links: when you yield a Request in a callback method, Scrapy will schedule that
# request to be sent and register a callback method to be executed when that request finishes.
#

# class QuotesSpider(scrapy.Spider):
#     name = "quotes"
#     start_urls = [
#         'http://quotes.toscrape.com/page/1/',
#     ]
#
#     def parse(self, response):
#         for quote in response.css('div.quote'):
#             yield {
#                 'text': quote.css('span.text::text').get(),
#                 'author': quote.css('small.author::text').get(),
#                 'tags': quote.css('div.tags a.tag::text').getall(),
#             }
#
#         next_page = response.css('li.next a::attr(href)').get()
#         if next_page is not None:
#             next_page = response.urljoin(next_page)
#             yield scrapy.Request(next_page, callback=self.parse)


#
# V6
# You can provide command line arguments to your spiders by using the -a option when running them.
# In this example, the value provided for the tag argument will be available via self.tag.
# You can use this to make your spider fetch only quotes with a specific tag, building the URL based on the argument
#
# scrapy crawl quotes -o output/quotes-humor.json -a tag=humor
#

# class QuotesSpider(scrapy.Spider):
#     name = "quotes"
#
#     def start_requests(self):
#         url = 'http://quotes.toscrape.com/'
#         tag = getattr(self, 'tag', None)
#         if tag is not None:
#             url = url + 'tag/' + tag
#         yield scrapy.Request(url, self.parse)
#
#     def parse(self, response):
#         for quote in response.css('div.quote'):
#             yield {
#                 'text': quote.css('span.text::text').get(),
#                 'author': quote.css('small.author::text').get(),
#             }
#
#         next_page = response.css('li.next a::attr(href)').get()
#         if next_page is not None:
#             yield response.follow(next_page, self.parse)
