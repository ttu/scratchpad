import unittest

from validations import *

class UserNameValidationTests(unittest.TestCase):

    def setUp(self):
		pass

    def test_userName_NotValid(self):
		self.assertFalse(valid_username('no'))

    def test_userName_Valid(self):
		self.assertTrue(valid_username('timmy'))

class PasswordValidationTests(unittest.TestCase):

    def test_password_NotValid(self):
		self.assertFalse(valid_password('no'))

    def test_password_Valid(self):
		self.assertTrue(valid_password('timmy'))
		
if __name__ == '__main__':
	#unittest.main()
	suite1 = unittest.TestLoader().loadTestsFromTestCase(UserNameValidationTests)
	suite2 = unittest.TestLoader().loadTestsFromTestCase(PasswordValidationTests)
	suite = unittest.TestSuite([suite1, suite2])
	unittest.TextTestRunner(verbosity=2).run(suite)