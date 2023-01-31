from pynput.keyboard import Key, Listener
import numpy as np
import random as rd

def inittab():
    grille=[]
    for i in range (0,4):
        grille.append(['*','*','*','*'])
    remplir(grille)
    remplir(grille)
    return grille

def remplir(grille):
    liste_chiffrededepart=[2,4]
    premierchiffre=rd.randint(0,3)
    secondchiffre=rd.randint(0,3)
    while grille[premierchiffre][secondchiffre]!='*':
        premierchiffre=rd.randint(0,3)
        secondchiffre=rd.randint(0,3)
    grille[premierchiffre][secondchiffre]=rd.choice(liste_chiffrededepart)
    return grille

def affichertab(grille):
    for i in range(len(grille)):
        for j in range(len(grille[i])):
            print(grille[i][j],end='\t')
        print('\n')


def on_press(key):
    print('{0} pressed'.format(
        key))

def on_release(key):
    print('{0} release'.format(
        key))
    if key == Key.down:
        # Stop listener
        return False

def deplacement(grille):
    flag=True
    with Listener(
            on_release=on_release) as listener:
        listener.join()
    if True:
        for i in range(len(grille)):
            for j in range(len(grille[i])):
                flag=True
                choix=grille[len(grille)-1-i][j]
                k=i
                while flag:
                    flag=False
                    if grille[len(grille)-1-k][j]!='*' and len(grille)-1-k+1<len(grille) and grille[len(grille)-1-k+1][j]=='*':
                        grille[len(grille)-1-k+1][j]=grille[len(grille)-1-k][j]
                        grille[len(grille)-1-k][j]='*'
                        k-=1
                        flag=True
        for i in range(len(grille)):
            for j in range(len(grille[i])):
                if grille[i][j]!='*' and i+1<len(grille) and grille[i+1][j]!='*' and grille[i][j]==grille[i+1][j]:
                    grille[i+1][j]*=grille[i][j]
                    grille[i][j]='*'
        remplir(grille)
    print('\n\n')
    return grille

def gameover(grille):
    flag=True
    (i,j)=(0,0)
    while i<len(grille) and flag:
        while j<len(grille[i]) and flag:
            if grille[i][j]=='*':
                flag=False
            j+=1
        i+=1
    return flag


def Jeux ():
    grille=inittab()
    affichertab(grille)
    flag=gameover(grille)
    while flag==False:
        affichertab(deplacement(grille))
        flag=gameover(grille)

Jeux()
