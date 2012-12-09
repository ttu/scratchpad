'The ATM on their home island can give only $5 bills, which means that the machine will not give any bill not divisible by $5. In addition to that, the commission for each cashing out is $0.5 + 1% from the withdrawn cash plus the robots cannot go beyond the card’s balance. After each operation, the bank rounds the balance to lower whole value (57.9 = 57, 61.1 = 61)'

# Withdraw without any incident 
# 120 - 10 - 0.5 - 1% = floor(109.4) = 109
# 109 - 20 - 0.5 - 1% = floor(88.3) = 88
# 88 - 30 - 0.5 - 1% = floor(57.2) = 57

def checkio(data):
    balance, withdrawal = data
    
    #loop all withdrawals
    for current in withdrawal:
        #Accept only 5s and must have enough balance
        if current % 5 == 0 and current < balance:
            balance = balance - counttotal(current)
            #Round down
            balance = int(balance)

    print(balance)    
    return balance
    
def counttotal(data):
    amount = data + 0.5 + (data * 0.01)
    return amount
    
if __name__ == '__main__':
    assert checkio([120, [10 , 20, 30]]) == 57, 'First'

    # With one Insufficient Funds, and then withdraw 10 $
    assert checkio([120, [200 , 10]]) == 109, 'Second'

    #with one incorrect amount
    assert checkio([120, [3, 10]]) == 109, 'Third'

    assert checkio([120, [200, 119]]) == 120 , 'Fourth'

    assert checkio([120, [120, 10, 122, 2, 10, 10, 30, 1]]) == 56, "It's mixed all base tests"
    
    print('All Ok')