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
    levelCounter = 0
    rowCounter = 1
    levelCards = {}
    scoreChain = {}
    
    for row in rows:
        rowCounter += 1
        qualities = {}
        
        if ( row['Level'] != str(levelCounter) ): # end of current level's data reached
            possible, scores = findAllPossibleCubes( levelCards )
            findRemainingCubes(possible)
            # SORT CHAINS FROM LONGEST TO SHORTEST
            sortedKeys = cubeChain.keys()
            sortedKeys.sort()
            chainsWithoutSubsets = cubeChain.copy() # working copy, so we don't mutate the thing we're iterating over
            for key in sortedKeys:
                if key == sortedKeys[-1]:
                    break
                for chain1 in cubeChain[key+1]:
                    for chain2 in cubeChain[key]:
                        unique = False
                        for cube in chain2:
                            if chain1.count(cube) == 0:
                                unique = True
                                break
                        # SAFE TO REMOVE
                        if unique == False and chain1 != chain2 and chainsWithoutSubsets[key].count(chain2) > 0:
                            chainsWithoutSubsets[key].remove(chain2)
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
                possibleLengths += str(key) + '(' + str(len(chainsWithoutSubsets[key])) + ') '
            sortedScores = scoreChain.keys()
            sortedScores.sort()
            for score in sortedScores:
                possibleScores += str(score) + '(' + str(len(scoreChain[score])) + ') '
            outData.write( str(levelCounter) + ',' + 'XX,' + 'XX,' + 'XXXXXXX,' + possibleLengths + ',' + possibleScores + '\n')
            for key in sortedKeys:
                #print str(key) + ': ' + str(len(chainsWithoutSubsets[key]))
                for chain in chainsWithoutSubsets[key]:
                    #print chain
                    score = 0
                    contents = ''
                    for cube in chain:
                        score += scores[possible.index(cube)]
                        contents += '['
                        for piece in cube:
                            contents += piece + ' '
                        contents += '] '
                    outData.write( str(levelCounter) + ',' + str(key) + ',' + str(score) + ',' + contents + '\n')
            levelCards.clear()
            cubeChain.clear()
            scoreChain.clear()
            levelCounter = row['Level']

        if levelCounter == 1:
            print 'hello!'
        for entry in row: # add qualities to a card
            qualities.update({entry: str(row[entry])})
        levelCards.update({str(rowCounter): qualities})
        #print levelCards

def findAllPossibleCubes( pDict ):
    cubes = []
    scores = []
    scoringQualities = {}
    deck = pDict.values()
    cardNumbers = pDict.keys()
    if (len(deck) < 3):
        return cubes, scores
    for card in deck:
        for quality in card.keys():
            if quality == 'Orientation':
                continue
            if not scoringQualities.has_key(quality):
                scoringQualities[quality] = []
            if scoringQualities[quality].count(card[quality]) == 0:
                scoringQualities[quality].append(card[quality])
    for i in range(0, len(deck)-2):
        card1 = deck[i]
        for j in range(i+1, len(deck)-1):
            card2 = deck[j]
            for k in range(j+1, len(deck)):
                score = 0
                card3 = deck[k]
                isCube = False
                if card1['Orientation'] == card2['Orientation'] or card1['Orientation'] == card3['Orientation'] or card2['Orientation'] == card3['Orientation']:
                    continue
                for quality in card1.keys():
                    isSame = False
                    isDifferent = False
                    if (card1[quality] == card2[quality] and card1[quality] == card3[quality] and card2[quality] == card3[quality]):
                        isSame = True
                        #isDifferent = False
                        if len(scoringQualities[quality]) == 3:
                               score += 1
                    elif (card1[quality] != card2[quality] and card1[quality] != card3[quality] and card2[quality] != card3[quality]):
                        #isSame = False
                        isDifferent = True
                        if quality != 'Orientation' and len(scoringQualities[quality]) == 3:
                            score += 3
                    #isCube = isSame or isDifferent
                    if not (isSame or isDifferent):
                        break
                #print str(isSame) + " " + str(isDifferent)
                if not (isSame or isDifferent):
                    continue
                cube = [cardNumbers[i], cardNumbers[j], cardNumbers[k]]
                cube.sort()
                cubes.append(cube)
                # Special case -- a cube is always worth at least 1 point
                if score == 0:
                    score = 1
                scores.append(score)
    return cubes, scores



def findRemainingCubes( pFreshList, pUsedList=[[]], pCounter=0 ):
    global cubeChain
    newCounter = pCounter
    newUsedList = pUsedList[:]
    if len(pFreshList) > 0:
        isTerminus = True
        for possibleCube in pFreshList:
            isTerminus = True
            alreadyInUsedList = False
            newUsedList = pUsedList[:]
            newFreshList = pFreshList[pFreshList.index(possibleCube)+1:]
            newCounter = pCounter
            for piece in possibleCube:
                for usedCube in newUsedList:
                    if usedCube.count(piece) > 0:
                        alreadyInUsedList = True
                        break
                if alreadyInUsedList == True:
                    break
            if alreadyInUsedList == False:
                newUsedList.append(possibleCube)
                newCounter += 1
                isTerminus = (newCounter == findRemainingCubes( newFreshList, newUsedList, newCounter ))
        if isTerminus == True:
            newUsedList.sort()
            if cubeChain.has_key(newCounter):
                cubeChain[newCounter].append(newUsedList[1:])
            else:
                cubeChain[newCounter] = [newUsedList[1:]]
            #print str(pCounter) + ': ' + str(pUsedList[1:])
            
    #else:
    #    print str(pCounter) + ': ' + str(pUsedList[1:])
        
    return newCounter
        

#xmlData.write("</Level>") # finish the very last level description
#levelCounter += 1
#xmlData.close()

#print "Level files generated: " + str(levelCounter + 1)
#print "Cards generated: " + str(numCards)
#print "Qualities generated: " + str(numQualities)

if __name__ == '__main__':
    main()

outData.close()
