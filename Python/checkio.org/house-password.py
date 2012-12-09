'Password is considered to be strong enough if its length is more than or equal 10 symbols and it has at least one digit, one upper and one lower case letters.'

def checkio(data):
    'Return True if password strong and False if not'
    
    #Length at least 10
    if len(data) < 10:
        return False
            
    #If is all numeric fail
    if data.isdigit() == True:
        return False;    
    
    oneNumber = False
    oneTitle = False
    oneLower = False
    for s in list(data):
        #Check that has at least one number
        if s.isdigit() == True:
            oneNumber = True
        #Check that has at least one capital alphabet
        if s.istitle() == True:
            oneTitle = True
        #Check that has at least one lower alphabet
        if s.isdigit() == False and s.istitle() == False:
            oneLower = True
    if oneNumber == False or oneTitle == False or oneLower == False:
        return False
        
    return True
    

if __name__ == '__main__':
    assert checkio('A1213pokl')==False, 'First'
    assert checkio('bAse730onE4')==True, 'Second'
    assert checkio('asasasasasasasaas')==False, 'Third'
    assert checkio('QWERTYqwerty')==False, 'Fourth'
    assert checkio('123456123456')==False, 'Fifth'
    assert checkio('QwErTy911poqqqq')==True, 'Sixth'
    print('All ok')