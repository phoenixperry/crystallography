import csv
import re
import os
import string

outFilePrefix = './analysis/'
csvFile = './crystallonlevels.csv'

pNote = re.compile(r'NOTE.*',re.IGNORECASE)


#p1 = re.compile(r'(.+)(^\t<group name="Goals".*?\t</group>)(.+</objects>)',re.MULTILINE+re.DOTALL)

##m = re.search(p1, xmlDataIn.read())

##m = xmlDataIn.read()

##xmlData.write(m.group(1))

if not os.path.exists(outFilePrefix):
    os.makedirs(outFilePrefix)

rows = []
csvData = csv.DictReader(open(csvFile, 'rb'))
for row in csvData:
    # delete the "notes" column
    #del row
    keys = row.keys()
    for key in keys:
        if ( re.match(pNote, key) != None or key == ''):
            #print key
            del row[key]
    if row['Level'] != '':
        rows.append(row)
        #print row

levelNum = 0
numCards = 0
numQualities = 0

#prepare the very first level description
outFile = outFilePrefix + "crystallon_analysis.csv"
outData = open(outFile, 'w')
outData.write("Level,Length,Points,Contents,Lengths,Scores\n")

cubeChain = {}

def main():
    global levelNum
    rowCounter = 1
    levelCards = {}
    scoreChain = {}
    
    for row in rows:
        rowCounter += 1
        qualities = {}
        
        if ( row['Level'] != str(levelNum) ): # end of current level's data reached
            possible, scores = findAllPossibleCubes( levelCards )
            if int(levelNum) == 3:
                print possible
            findRemainingCubes(possible)
            # SORT CHAINS FROM LONGEST TO SHORTEST
            sortedKeys = cubeChain.keys()
            sortedKeys.sort()
            chainsWithoutSubsets = cubeChain.copy() # working copy, so we don't mutate the thing we're iterating over
            if int(levelNum) == 3:
                print '--------------------'
                print sortedKeys
#                for key in sortedKeys:
#                    print str(key) + ": " + str(cubeChain[key])
#                    print ""
            for key in sortedKeys:
                if key == sortedKeys[-1]:
                    break
                if int(levelNum) == 3:
                    print str(key+1) + ", " + str(key)
                cubeChain[key] = removeSubsets( cubeChain[key+1], cubeChain[key] )
                chainsWithoutSubsets[key] = cubeChain[key]
#                for chain1 in cubeChain[key+1]:
#                    for chain2 in cubeChain[key]:
#                        unique = False
#                        for cube in chain2:
#                            # if a short chain contains a cube that is not found in a long chain, the short chain is not a subset
#                            if chain1.count(cube) == 0:
#                                unique = True
#                                if int(levelNum) == 3:
#                                    print str(chain1) + "\n" + str(chain2) + ": " + str(cube) + " " + str(chain1.count(cube)) + "\n"
#                                break
#                            else:
#                                if int(levelNum) == 3:
#                                    print str(chain1) + "\n" + str(chain2) + ": " + str(cube) + " " + str(chain1.count(cube)) + "\n"
#                        # if a short chain is found to be a subset, remove it
#                        if unique == False and chainsWithoutSubsets[key].count(chain2) > 0:
#                            chainsWithoutSubsets[key].remove(chain2)
            for key in chainsWithoutSubsets.keys():
                for chain in chainsWithoutSubsets[key]:
                    score = 0
                    for cube in chain:
                        score += scores[possible.index(cube)]
                    if not scoreChain.has_key(score):
                        scoreChain[score] = []
                    scoreChain[score].append(chain)
            possibleLengths = ''
            possibleScores = ''
            for key in sortedKeys:
                if len(chainsWithoutSubsets[key]) > 0:
                    possibleLengths += str(key) + '(' + str(len(chainsWithoutSubsets[key])) + ') '
            sortedScores = scoreChain.keys()
            sortedScores.sort()
            for score in sortedScores:
                possibleScores += str(score) + '(' + str(len(scoreChain[score])) + ') '
            outData.write( str(levelNum) + ',' + 'XX,' + 'XX,' + 'XXXXXXX,' + possibleLengths + ',' + possibleScores + '\n')
            for key in sortedKeys:
#                if ( int(levelNum) == 5):
#                    print str(key) + ': ' + str(len(chainsWithoutSubsets[key]))
                for chain in chainsWithoutSubsets[key]:
#                    if ( int(levelNum) == 5):
#                        print str(chain)
                    score = 0
                    contents = ''
                    for cube in chain:
                        score += scores[possible.index(cube)]
                        contents += '['
                        for piece in cube:
                            contents += piece + ' '
                        contents += '] '
                    outData.write( str(levelNum) + ',' + str(key) + ',' + str(score) + ',' + contents + '\n')
                    if int(levelNum) == 3:
                        print str(key) + ", " + str(score) + ": " + contents
            levelCards.clear()
            cubeChain.clear()
            scoreChain.clear()
            levelNum = row['Level']

        for entry in row: # add qualities to a card
            qualities.update({entry: str(row[entry])})
        levelCards.update({str(rowCounter): qualities})
        #print levelCards

def removeSubsets ( longChains, shortChains ):
    for longChain in longChains:
        for shortChain in shortChains:
            if shortChain == None:
                continue
            match = 0
            for cube in shortChain:
                if longChain.count(cube) > 0:
                    match += 1
            if len(shortChain) == match:
                shortChains[shortChains.index(shortChain)] = None
    while shortChains.count(None):
        shortChains.remove(None)
    return shortChains
            
    

def findAllPossibleCubes( pDict ):
    global levelNum
    cubes = []
    scores = []
    scoringQualities = {}
    deck = pDict.values()
    cardNumbers = pDict.keys()
    if (len(deck) < 3):
        return cubes, scores
    
    # Build list of scoring qualities
    for card in deck:
        for quality in card.keys():
            if quality == 'Orientation':
                continue
            if not scoringQualities.has_key(quality):
                scoringQualities[quality] = []
            if scoringQualities[quality].count(card[quality]) == 0:
                scoringQualities[quality].append(card[quality])
                
    # Build a list of all possible cubes, valid or not, filtering only orientation
    for i in range(0, len(deck)-2):
        card1 = deck[i]
        for j in range(i+1, len(deck)-1):
            card2 = deck[j]
            for k in range(j+1, len(deck)):
                score = 0
                card3 = deck[k]
                if card1['Orientation'] == card2['Orientation'] or card1['Orientation'] == card3['Orientation'] or card2['Orientation'] == card3['Orientation']:
                    continue
                cube = [cardNumbers[i], cardNumbers[j], cardNumbers[k]]
                cube.sort()
                cubes.append(cube)

    cubes.sort()
#    if int(levelNum) == 5:
#        print cubes
#    sortedCubes = cubes[:]

    # Remove invalid cubes and determine the point value of all the valid cubes
    for cube in cubes:
        score = calculateScore( [deck[cardNumbers.index(cube[0])], deck[cardNumbers.index(cube[1])], deck[cardNumbers.index(cube[2])]], scoringQualities )
        if score == -1:
#            if int(levelNum) == 5:
#                print "Illegal Cube"
#                print "----"
            cubes[cubes.index(cube)] = None
        else:
            if score == 0:
                score = 1
            scores.append(score)
    while cubes.count(None) > 0:
        cubes.remove(None)
        
    return cubes, scores

def calculateScore ( pCube, pScoringQualities ):
    global levelNum
    score = 0
    for quality in pCube[0].keys():
        isSame = False
        isDifferent = False
        if (pCube[0][quality] == pCube[1][quality] and pCube[0][quality] == pCube[2][quality] and pCube[1][quality] == pCube[2][quality]):    
            isSame = True
            if len(pScoringQualities[quality]) == 3:
                score += 1
        elif (pCube[0][quality] != pCube[1][quality] and pCube[0][quality] != pCube[2][quality] and pCube[1][quality] != pCube[2][quality]):
            isDifferent = True
            if quality != 'Orientation' and len(pScoringQualities[quality]) ==3:
                score += 3
#        if int(levelNum) == 5:
#            print str(quality) + ": " + str(isSame) + " " + str(isDifferent)
        if not (isSame or isDifferent):
            score = -1
            break
#    if int(levelNum) == 5:
#        print "-----------------------------"
    return score
        

def findRemainingCubes( pFreshList, pUsedList=[[]], pCounter=0 ):
    global cubeChain
    global levelNum
    newCounter = pCounter # this is the "depth" of the recursion, i.e. how many cubes we've made already
    newUsedList = pUsedList[:]
    if len(pFreshList) > 0:
        isTerminus = True
        for possibleCube in pFreshList:
            newUsedList = pUsedList[:] # make a copy of used list
            newFreshList = pFreshList[pFreshList.index(possibleCube)+1:] # make a copy of the fresh list including everything after this cube
            newCounter = pCounter

            # using this cube...
            newUsedList.append(possibleCube)
            newCounter += 1
            
            # remove any cubes that could not be formed after making
            # this cube from the copied fresh list
            for freshCube in newFreshList:
                for piece in possibleCube:
                    if freshCube.count(piece) > 0:
                        newFreshList[newFreshList.index(freshCube)] = None
                        break

            # clean up the list by removing all the null items
            while newFreshList.count(None) > 0:
                newFreshList.remove(None)
                        
            # if there are more possible cubes, explore deeper
#            if (len(newFreshList) > 0):
            if True:
                isTerminus = pCounter == findRemainingCubes( newFreshList, newUsedList, newCounter )
                
#    else:
    newUsedList.sort()
    if cubeChain.has_key(pCounter):
        cubeChain[pCounter].append(pUsedList[1:])
    else:
        cubeChain[pCounter] = [pUsedList[1:]]
#    if int(levelNum) == 5:
#        print str(pCounter) + ': ' + str(pUsedList[1:])

    
#    if pCounter == 0:
#        if int(levelNum) == 5:
#            for key in cubeChain:
#                print str(key) + ": "
#                for cube in cubeChain[key]:
#                    print str(cube)
#                print ""
    
    return newCounter


if __name__ == '__main__':
    main()

outData.close()
