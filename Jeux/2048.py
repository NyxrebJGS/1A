from pynput import keyboard
import numpy as np
import random as rd

def on_press(key):
    try:
        print('alphanumeric key {0} pressed'.format(
            key.char))
    except AttributeError:
        print('special key {0} pressed'.format(
            key))

def on_release(key):
    print('{0} released'.format(
        key))
    if key == keyboard.Key.esc:
        # Stop listener
        return False

def inittab():
    grille=[]
    for i in range (0,4):
        grille.append(['*','*','*','*'])
    liste_chiffrededepart=[2,4]
    for i in range (0,2):
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

def deplacement(grille):
    mouv=keyboard.Key.esc
    with keyboard.Events() as events:
        for event in events:
            if event.key == keyboard.Key.up or event.key == keyboard.Key.down or event.key == keyboard.Key.left or event.key == keyboard.Key.right:
                mouv=event.key
                break
    if mouv==keyboard.Key.down:
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
                        k+=1
                        flag=True

    print('\n\n')
    return grille



grille=inittab()
affichertab(grille)
deplacement(grille)
affichertab(grille)
