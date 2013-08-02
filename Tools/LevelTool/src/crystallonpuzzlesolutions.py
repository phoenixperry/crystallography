import csv
import re
import os
import string

outFilePrefix = './analysis/'
csvFile = './crystalloncards.csv'
populationCsv = './crystallonlevels.csv'

# GET MAX POPULATION FOR EACH LEVEL
PopDict = {}

rows = []
csvData = csv.DictReader(open(populationCsv, 'rb'))
for row in csvData:
    if row['StandardPop'] != '':
        PopDict[row['Level']] = 3 + int(row['StandardPop'])
    else:
        PopDict[row['Level']] = 18

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
numCards = 0
numQualities = 0
allPossibleCubes = []
allPossibleCubeScores = {}
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

    population = PopDict[rows[0]['Level']]
    
    for row in rows:
        rowCounter += 1
        qualities = {}
        
        if ( row['Level'] != str(levelNum) ): # end of current level's data reached
            deck = levelCards

            # INITIAL LEVEL SETUP
            SetUpLevel( deck ) #this populates deck and cardNumbers

            # FIND ALL POSSIBLE PLAYTHROUGHS
            
            if( len(cardNumbers) > population ):
                SimulatePlay( cardNumbers[:population], cardNumbers[population:] )
            else:
                SimulatePlay( cardNumbers, [] )


            # REMOVE NON-UNIQUE PLAYTHROUGHS
            sortedKeys = cubeChain.keys()
            sortedKeys.sort()
            sortedKeys.reverse()

            goodChains = cubeChain[sortedKeys[0]][:]
            stats = str(levelNum) + " -- " + str(sortedKeys[0]) + ": " + str(len(cubeChain[sortedKeys[0]])) + " | "
            for key in sortedKeys:
                if key == sortedKeys[0]:
                    continue
                cubeChain[key] = RemoveSubsets( goodChains, cubeChain[key] )
                length = len(cubeChain[key])
                if length > 0:
                    stats += str(key) + ": " + str(length) + " | "
                if len(cubeChain[key]) > 0:
                    goodChains.extend(cubeChain[key])

            print stats
            
            # CALCULATE TOTAL SCORES FOR CUBE UNIQUE CHAINS
            BuildScoreChains( cubeChain ) # this populates scoreChain

            
            
            
            # OUTPUT HEADER ROW FOR LEVEL
            possibleLengths = ''
            possibleScores = ''
            sortedKeys.reverse()
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
                        score += allPossibleCubeScores[str(cube)]
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
            if str(levelNum) == '999':
                population = 15
            else:
                population = PopDict[str(levelNum)]

        # GATHER CARD DATA
        for entry in row: # add qualities to a card
            qualities.update({entry: str(row[entry])})
        levelCards.update({str(rowCounter): qualities})

    # DONE
    outData.close()

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
                if cube in longChain:
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
    allPossibleCubeScores = {}

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
    if len(deck) != len(pOnScreen):
        Cubes = FindPossibleCubes( pOnScreen, False )
    else:
        Cubes = allPossibleCubes
    
    # If no possible cubes can be made from this on-screen card population, return
    counter = len(Cubes)
    if( counter == 0 ):
        if cubeChain.has_key(len(pPreviousCubes)):
            cubeChain[counter].append(pPreviousCubes[:])
        else:
            cubeChain[counter] = [pPreviousCubes[:]]
        return
    
    # Recursively create all possible paths to having no cards off screen
    if ( len(pLeftover) > 0 ):
        newLeftover = []
        adds = []

        # draw the next 3 cards and add them to the screen
        if ( len(pLeftover) > 3):
            adds = pLeftover[:2]
            newLeftover = pLeftover[3:]
        else:
            adds = pLeftover[:]

        # Recursion: Simulate Play based on every cube the player could possibly make with this screen population
        for cube in Cubes:
            newOnScreen = [card for card in pOnScreen if int(card) not in cube]
            if len(adds) > 0:
                newOnScreen.extend(adds)
            newCubes = pPreviousCubes[:]
            newCubes.append(cube)
            
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
    global scoringQualities
    global deck
    global cardNumbers
    cubes = []
    scores = {}

    if (len(pCardsOnScreen) >= 3):
        
        # Build a list of all possible cubes, valid or not, filtering only orientation
        cards = []
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
                    if cube not in cubes:
                        score = CalculateScore( [deck[str(cube[0])], deck[str(cube[1])], deck[str(cube[2])]], scoringQualities )
                        if score != -1:
                            cubes.append(cube)
                            if pReturnScores:
                                scores[str(cube)] = max(score, 1)

    # Put the cubes in numerical order to make the output easier to read
    cubes.sort()

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
        uniqueQualities = len(set([piece[quality] for piece in pCube]))
        # ALL SAME
        if uniqueQualities == 1:
            if len(pScoringQualities[quality]) == 3:
                score += 1
        # ALL DIFFERENT
        elif uniqueQualities == 3:
            if quality != 'Orientation' and len(pScoringQualities[quality]) == 3:
                score += 3
        # ILLEGAL CUBE
        else:
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
    if length in cubeChain.keys():
        if pUsedList not in cubeChain[length]:
#        try:
#            cubeChain[length].index(pUsedList)
#        except ValueError:
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
                score += allPossibleCubeScores[str(cube)]
            if not scoreChain.has_key(score):
                scoreChain[score] = []
            scoreChain[score].append(chain)


if __name__ == '__main__':
    main()
