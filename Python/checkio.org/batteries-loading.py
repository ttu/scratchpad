import itertools;

def checkio(stones):
    '''
    minimal possible weight difference between stone piles
    '''
    total = 0
    for cur in stones:
        total += cur
        
    bestval = -1
    
    for i in range(0,len(stones)):
        for comb in itertools.combinations(stones,i):
            weight = 0
            for item in comb:
                weight += item
            d = diff(total - weight, weight)
            if bestval == -1 or d < bestval:
                bestval = d
                       
    return bestval
    
def diff(a,b):
    if a >= b:
        return a - b
    else:
        return b - a
    
if __name__ == '__main__':
    assert checkio([10,10]) == 0, 'First, with equal weights'
    assert checkio([10]) == 10, 'Second, with a single stone'
    assert checkio([5, 8, 13, 27, 14]) == 3, 'Third'
    assert checkio([5,5,6,5]) == 1, 'Fourth'
    assert checkio([12, 30, 30, 32, 42, 49]) == 9, 'Fifth'
    assert checkio([1, 1, 1, 3]) == 0, "Six, don't forget - you can hold different quantity of parts"
    print ('All is ok')