'Input: Integer number. From 0 to 1000'
'Output: String representation of this number'

sto = ['', 'one', 'two', 'three' ,'four', 'five', 'six', 'seven', 'eight', 'nine', 'ten',
'eleven', 'twelve' , 'thirteen', 'fourteen', 'fifteen', 'sixteen', 'seventeen', 'eighteen', 'nineteen']

stt = ['', '', 'twenty', 'thirty', 'forty', 'fifty', 'sixty', 'seventy', 'eighty', 'ninety']

def checkio(number):
    #return 'string representation of n'
    
    thousands = int(number / 1000)
    
    number = number - (thousands * 1000)
    
    hundreds = int(number / 100)
    
    number = number - (hundreds * 100)
    
    tens = int(number / 10)
    
    number = number - (tens * 10)
    
    ones = number
    
    returnvalue = ''
    
    if thousands > 0:
        returnvalue = sto[thousands] + ' thousand'
    if hundreds > 0:
        if returnvalue != '':
            returnvalue += ' '
        returnvalue += sto[hundreds] + ' hundred'
    #Values under 20 go normally
    if tens > 1:
        if returnvalue != '':
            returnvalue += ' '
        returnvalue +=  stt[tens]
        if ones > 0:
            if returnvalue != '':
                returnvalue += ' '
            returnvalue += sto[ones]
    if tens < 2:
        #Add tens to ones
        ones += tens * 10
        
        if ones > 0 and returnvalue != '':
            returnvalue += ' '
            
        returnvalue += sto[ones]
        
    print(returnvalue)
    return returnvalue 
    
if __name__ == '__main__':
    assert checkio(4) == 'four', "First"
    assert checkio(133) == 'one hundred thirty three', "Second"
    assert checkio(12)=='twelve', "Third"
    assert checkio(101)=='one hundred one', "Fifth"
    assert checkio(212)=="two hundred twelve", "Sixth"
    assert checkio(40)=='forty', "Seventh, forty - it is correct"
    print('All ok')