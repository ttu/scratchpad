'Write a program that, depending on the Sofia’s and old man’s starting bargaining sums, the step on which they will increase or decrease the price during the argument, will calculate on which price they will agree. If the offer of Sofia, the price will be higher or equal to the price the old man, she will take the old man offer. And vice versa.'

def checkio(offers):
    '''
       the amount of money that Petr will pay for the ride
    '''
    initial_petr, raise_petr, initial_driver, reduction_driver = offers
    current_p = initial_petr
    current_d = initial_driver
    offer = current_p
    
    while current_p < current_d:
        next_p = current_p + raise_petr
        
        #If next offer is greater than drivers current
        if next_p >= current_d:
            offer = current_d
            break  
            
        current_p = next_p
        current_d -= reduction_driver
        offer = current_p
        
    return offer

if __name__ == '__main__':
    assert checkio([150, 50, 1000, 100]) == 450, 'First'
    assert checkio([150, 50, 900, 100]) == 400, 'Second'
    assert checkio([200, 100, 200, 100]) == 200, 'Third'
    assert checkio([500, 300, 700, 50]) == 700, 'Fourth'
    print('All is ok')