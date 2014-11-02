import webapp2

form = """
<form action="http://www.google.com/search">
    <input name="q">
    <input type="submit">
</form>
"""

# Response to: http://localhost:8080/
class MainPage(webapp2.RequestHandler):
    def get(self):
        #self.response.headers['Content-Type'] = 'text/plain'
        self.response.out.write(form)

# Response to: http://localhost:8080/testform?q=Hello%2C+World!
class TestHandler(webapp2.RequestHandler):
    def get(self):
        #q=self.request.get("q")
        #self.response.out.write(q)
		# Show whole request
        self.response.headers['Content-Type'] = 'text/plain'
        self.response.out.write(self.request)

app = webapp2.WSGIApplication([('/', MainPage),
                               ('/testform', TestHandler)],
                              debug=True)