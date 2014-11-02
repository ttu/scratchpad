import webapp2

form = """
<form method="post" action="/testform">
    <input name="q"> <input type="submit">
</form>
"""

class MainPage(webapp2.RequestHandler):
    def get(self):
        #self.response.headers['Content-Type'] = 'text/plain'
		self.response.out.write(form)

class TestHandler(webapp2.RequestHandler):
    def post(self):
		q =	self.request.get("q")
		self.response.out.write(q)
    def get(self):
        q = self.request.get("q")
        self.response.out.write(q)

app = webapp2.WSGIApplication([('/', MainPage),
					('/testform', TestHandler)],
					debug=True)
