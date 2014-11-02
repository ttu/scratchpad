import sys
sys.path.append(r"C:\Python27\Lib\site-packages")

import os
import webapp2
import jinja2
import logging
from google.appengine.ext import db

template_dir = os.path.join(os.path.dirname(__file__), 'templates')
jinja_env = jinja2.Environment(loader = jinja2.FileSystemLoader(template_dir), autoescape=True)

# Asterisks are used in argument naming for passing lists (*) and dictionaries (**)
class Handler(webapp2.RequestHandler):
	def write(self, *a, **kw):
		self.response.out.write(*a, **kw)
	def render_str(self, template, **params):
		t = jinja_env.get_template(template)
		return t.render(params)
	def render(self, template, **kw):
		self.write(self.render_str(template, **kw))

class MainPage(Handler):
	def render_front(self, title="", art="", error=""):
		logging.info('render_front')
		self.render("front.html", title=title, art=art, error = error)
	def get(self):
		self.render("front.html")
	def post(self):
		title = self.request.get("title") 
		art = self.request.get("art")
		logging.info('title' + title + ' art' + art)
		if title and art:
			self.write("thanks!")
		else:
			error = "we need both a title and some artwork!" 
			self.render_front(title, art, error)

app = webapp2.WSGIApplication([('/', MainPage)], debug=True)