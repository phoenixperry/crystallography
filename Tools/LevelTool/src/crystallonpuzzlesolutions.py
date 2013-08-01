import csv
import re
import os
import string

outFilePrefix = './analysis/'
csvFile = './crystalloncards.csv'

pNote = re.compile(r'NOTE.*',re.IGNORECASE)

if not os.path.exists(outFilePrefix):
    os.makedirs(outFilePrefix)

rows = []
csvData = csv.DictReader(open(csvFile, 'rb'))
for row in csvData:
    keys = row.keys()
    for key in keys:
        if ( re.match(pNote, key) != None or key == ''):
            del row[key]
    if row['Level'] != '':
        rows.append(row)

levelNum = 0
maxPop = 18
numCards = 0
numQualities = 0
allPossibleCubes = []
allPossibleCubeScores = []
deck = {}
cardNumbers = []
scoringQualities = {}
cubeChain = {}
scoreChain = {}


#prepare the very first level description
outFile = outFilePrefix + "crystallon_analysis.csv"
outData = open(outFile, 'w')
outData.write("Level,Length,Points,Contents,Lengths,Scores\n")

def main():
    global levelNum
    rowCounter = 1
    levelCards = {}
    global scoreChain
    global cubeChain
    global deck
    
    for row in rows:
        rowCounter += 1
        qualities = {}
        
        if ( row['Level'] != str(levelNum) ): # end of current level's data reached
            print levelNum
            deck = levelCards

            # INITIAL LEVEL SETUP
            SetUpLevel( deck ) #this populates deck and cardNumbers

            # FIND ALL POSSIBLE PLAYTHROUGHS
            if( len(cardNumbers) > 18 ):
                SimulatePlay( cardNumbers[:17], cardNumbers[18:] )
            else:
                SimulatePlay( cardNumbers, [] )


            # REMOVE NON-UNIQUE PLAYTHROUGHS
            sortedKeys = cubeChain.keys()
            sortedKeys.sort()
            
            for key in sortedKeys:
                if key == sortedKeys[-1]:
                    break
                cubeChain[key] = RemoveSubsets( cubeChain[key+1], cubeChain[key] )

            # CALCULATE TOTAL SCORES FOR CUBE UNIQUE CHAINS
            BuildScoreChains( cubeChain ) # this populates scoreChain

            # OUTPUT HEADER ROW FOR LEVEL
            possibleLengths = ''
            possibleScores = ''
            for key in sortedKeys:
                if len(cubeChain[key]) > 0:
                    possibleLengths += str(key) + '(' + str(len(cubeChain[key])) + ') '
            sortedScores = scoreChain.keys()
            sortedScores.sort()
            for score in sortedScores:
                possibleScores += str(score) + '(' + str(len(scoreChain[score])) + ') '
            outData.write( str(levelNum) + ',' + 'XX,' + 'XX,' + 'XXXXXXX,' + possibleLengths + ',' + possibleScores + '\n')

            # OUTPUT EACH POSSIBLE CUBE CHAIN, IT'S LENGTH, AND SCORE
            for key in sortedKeys:
                for chain in cubeChain[key]:
                    score = 0
                    contents = ''
                    for cube in chain:
                        score += allPossibleCubeScores[allPossibleCubes.index(cube)]
                        contents += '['
                        for piece in cube:
                            contents += str(piece) + ' '
                        contents += '] '
                    outData.write( str(levelNum) + ',' + str(key) + ',' + str(score) + ',' + contents + '\n')

            # CLEAN UP
            levelCards.clear()
            cubeChain.clear()
            scoreChain.clear()
            levelNum = row['Level']

        # GATHER CARD DATA
        for entry in row: # add qualities to a card
            qualities.update({entry: str(row[entry])})
        levelCards.update({str(rowCounter): qualities})
        

#################################################################################
# RemoveSubsets()
#
# Removes cube chains from shortChains that are subsets of a chain in longChains
#################################################################################
def RemoveSubsets ( longChains, shortChains ):
    for longChain in longChains:
        for shortChain in shortChains:
            if shortChain == None:
                continue
            match = 0
            for cube in shortChain:
                try:
                    longChain.index(cube)
                except ValueError:
                    continue
                else:
                    match += 1
            # IF EVERY CUBE IN THIS SHORT CHAIN IS CONTAINED IN ANY LONGER CHAIN, IT IS A SUBSET, SO MARK IT FOR REMOVAL
            if len(shortChain) == match:
                shortChains[shortChains.index(shortChain)] = None
    # REMOVE MARKED CHAINS FROM shortChains
    while shortChains.count(None):
        shortChains.remove(None)
    return shortChains


#######################################################################################
# SetUpLevel()
#
# Establishes level start conditions
#######################################################################################
def SetUpLevel( pDict ):
#    global deck
#    deck = pDict.values()
    global cardNumbers
    cardNumbers = pDict.keys()
    cardNumbers.sort()
    global scoringQualities
    scoringQualities = {}
    global allPossibleCubes
    allPossibleCubes = []
    global allPossibleCubeScores
    allPossibleCubeScores = []

    # BUILD DICTIONARY OF SCORING QUALITIES FOR THIS LEVEL
    for cardID in cardNumbers:
        card = deck[cardID]
        for quality in card.keys():
            if quality == 'Orientation':
                continue
            if not scoringQualities.has_key(quality):
                scoringQualities[quality] = []
            if scoringQualities[quality].count(card[quality]) == 0:
                scoringQualities[quality].append(card[quality])

    # FIND ALL POSSIBLE CUBES AND THEIR POINT VALUES
    allPossibleCubes, allPossibleCubeScores = FindPossibleCubes( cardNumbers, True )


#######################################################################################
# SimulatePlay()
#
# Finds all possible valid outcomes of a given deck state
#######################################################################################
def SimulatePlay ( pOnScreen, pLeftover, pPreviousCubes=[] ):
    global deck
    global cardNumbers
    global scoringQualities

    # Find all possible cubes that can be made with the cards on screen
    Cubes = FindPossibleCubes( pOnScreen, False )
    
    # If no possible cubes can be made from this on-screen card population, return
    counter = len(Cubes)
    if( counter == 0 ):
        if cubeChain.has_key(len(Cubes)):
            cubeChain[counter].append(pPreviousCubes[:])
        else:
            cubeChain[counter] = [pPreviousCubes[:]]
        return
    
    # Recursively create all possible playthroughs
    if ( len(pLeftover) > 0 ):
        newLeftover = []
        adds = []

        # draw the next 3 cards and add them to the screen
        if ( len(pLeftover) > 3):
            newLeftover = pLeftover[3:]
            adds = pLeftover[:2]
        else:
            adds = pLeftover[:]

        # Recursion: Simulate Play based on every cube the player could possibly make with this screen population
        for cube in Cubes:
            newOnScreen = pOnScreen[:]
            newCubes = pPreviousCubes[:]
            newCubes.append(cube)
            
            for card in cube:
                try:
                    newOnScreen.remove(str(card))
                except ValueError:
                    print newOnScreen, card
            newOnScreen.extend(adds)
            SimulatePlay( newOnScreen, newLeftover, newCubes  )
            
    # If there are no cards left in reserve, simulate all possible remaining playthroughs from this state
    else:
        BuildCubeChains( Cubes, pPreviousCubes, len(pPreviousCubes) )


##########################################################################################
# FindPossibleCubes()
#
# Returns a list of all possible valid cubes given a particular card population on screen
##########################################################################################
def FindPossibleCubes( pCardsOnScreen, pReturnScores=False ):
    global levelNum
    global maxPop
    global scoringQualities
    global deck
    global cardNumbers
    cubes = []
    scores = []

    if (len(pCardsOnScreen) < 3):
        return cubes, scores
                
    # Build a list of all possible cubes, valid or not, filtering only orientation
    for i in range(0, len(pCardsOnScreen)-2):
        card1 = deck[pCardsOnScreen[i]]
        for j in range(1, len(pCardsOnScreen)-1):
            card2 = deck[pCardsOnScreen[j]]
            for k in range(2, len(pCardsOnScreen)):
                card3 = deck[pCardsOnScreen[k]]
                if card1['Orientation'] == card2['Orientation'] or card1['Orientation'] == card3['Orientation'] or card2['Orientation'] == card3['Orientation']:
                    continue
                cube = [int(pCardsOnScreen[i]), int(pCardsOnScreen[j]), int(pCardsOnScreen[k])]
                # Put the sides in numerical order to make the output easier to read
                cube.sort()
                cubes.append(cube)

    # Put the cubes in numerical order to make the output easier to read
    cubes.sort()

    # Remove invalid cubes and determine the point value of all the valid cubes
    for cube in cubes:
        score = CalculateScore( [deck[str(cube[0])], deck[str(cube[1])], deck[str(cube[2])]], scoringQualities )
        if score == -1:
            cubes[cubes.index(cube)] = None
        else:
            if score == 0:
                score = 1
            scores.append(score)
    while cubes.count(None) > 0:
        cubes.remove(None)

    if pReturnScores:
        return cubes, scores
    else:
        return cubes

                
##############################################################################
# CalculateScore
#
# Determines the point value of a given cube in a given level
##############################################################################
def CalculateScore ( pCube, pScoringQualities ):
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
        if not (isSame or isDifferent):
            score = -1
            break
    return score

        
######################################################################################
# BuildCubeChains()
# 
# Finds any possible remaining cubes when there are no more cards left in reserve
######################################################################################
def BuildCubeChains( pFreshList, pUsedList=[], pCounter=0 ):
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
            isTerminus = pCounter == BuildCubeChains( newFreshList, newUsedList, newCounter )
                
    pUsedList.sort()
    length = len(pUsedList)
    if cubeChain.has_key(length):
        try:
            cubeChain[length].index(pUsedList)
        except ValueError:
#        if cubeChain[key].count(pUsedList) == 0:
            cubeChain[length].append(pUsedList[:])
    else:
        cubeChain[length] = [pUsedList[:]]
    
    return newCounter


################################################################################
# BuildScoreChains()
#
# Calculate total scores for each cube chain
################################################################################
def BuildScoreChains( pChains ):
    global scoreChain
    global allPossibleCubeScores
    global allPossibleCubes

    for key in pChains.keys():
        for chain in pChains[key]:
            score = 0
            for cube in chain:
                score += allPossibleCubeScores[allPossibleCubes.index(cube)]
            if not scoreChain.has_key(score):
                scoreChain[score] = []
            scoreChain[score].append(chain)


if __name__ == '__main__':
    main()

outData.close()
