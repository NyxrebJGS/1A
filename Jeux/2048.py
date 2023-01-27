
import numpy as np
import random as rd
from pynput.keyboard import Key, Controller


def inittab():
    grille=[]
    for i in range (0,4):
        grille.append(['*','*','*','*'])
    liste_chiffrededepart=[2,4]
    for i in range (0,2):
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
    try:
        print('alphanumeric key {0} pressed'.format(
            key.char))
    except AttributeError:
        print('special key {0} pressed'.format(
            key))

def coup(grille):
    on_press()

grille=inittab()
affichertab(grille)
coup(grille)
