TFOL
====

Repo de l'équipe pleine d'amour  :  
Julien *The Rock* Aymong  
Marie-France *Mousse* Miousse  
Ming *Ming* Dutilly  
Odric *Karibou* Plamondon  
Philippe *Fuck les gui* Groarke  
Alexandre *Stongeeee* St-Onge ( **Git Master** )  

#BREAKOUT

###Git Workflow

1. Fork sur GitHub
2. Clone **ton repo** sur ton ordi (<code>$ git clone git@github.com:<i>tonUsername</i>/TFOL.git</code>)
3. Ajoute **ce repo** comme remote upstream (<code>$ git remote add upstream git://github.com/Alex1602/TFOL.git</code>)
4. Crée une branche pour la feature sur laquelle tu veux travailler (<code>$ git checkout -b ajout-nouvelle-brique</code>)
5. Développe sur ta branche  *[le temps passe]*
5.1 Plus tu codes dans plusieurs fichier, plus sa risque d'être compliqué à faire le merge 
6. Fait **souvent** des commits sur la branche avec des noms clairs et indicatifs
7. Fetch upstream ($ git fetch upstream) 
8. Met à jour **ton master** (<code>git checkout master; git merge upstream/master</code>)  
8.1 Alternativement, on peux remplace 6 et 7 par (<code>$ git checkout master; git pull upstream</code>) *Phil me corrigera si je me trompe*  
9. Rebase ta branche (<code>$ git checkout ajout-nouvelle-brique; git rebase master)
10. Répète les étapes 5 à 9 jusqu'à complétion de la tâche
11. Push ta branche sur GitHub (<code>$ git push origin ajout-nouvelle-brique</code>)
12. Dans ton browser, sur la page de ta branche, appuie sur [Pull Request]
13.  ???
14.  Profit!
