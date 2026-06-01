# MCR-Project



## TODO
- [ _ ] item system
  - **[ X ]** spawn item
  - **[ X ]** item decoration
  - **[ X ]** wrapper
  - [ _ ] item player hook
  - [ _ ] item sell

- [ _ ] player
  - **[ X ]** player base movement
    - [ _ ] eat
    - [ _ ] move
  - [ _ ] hp system
  - [ _ ] take damage

- [ _ ] Shoopkeeper
  - [ _ ] ask money
  - [ _ ] power up
  - [ _ ] bet system
  - [ _ ] game over

- [ _ ] run hazard
  - [ _ ] hit box
    - [ _ ] laser
    - [ _ ] explosion
  - [ _ ] projectile


- [ _ ] animation
  - [ _ ] player
    - [ _ ] eat
    - [ _ ] move
    - [ _ ] death
    - [ _ ] spawn
  - [ _ ] shopkeeper
    - [ _ ] ask money
    - [ _ ] selling time
    - [ _ ] bet
    - [ _ ] gameover
  - [ _ ] hazard
    - [ _ ] hit box


### Note
Alors, 2 phase
première phase, la partie ramassage :
on joue un petit perso qui peu mordre (ce qui le fait faire un peiti dash comme la blasfemuse dans enter the gungeon)
Quand on est en train de mordre on peut manger les item sur le quel on est
c'est item on des effet diverse et varier et peuvent être cumulatife, aussi certain effet peuvent faire des déga

pas de barre de points de vie mais une barre de temps, quand on prend des déga cella baisse notre bare de temps

quand le temps tombe a zero, on est envoyer ver le shopkeeper et commence la seconde phase

lootbox are rare struct on the map which will spawn a bunch of junk with good rarity (like good décoration and stuff)


Shopkeeper :
en entrant chez le shopkeeper, on dois payer un montant (qui vas augmenter a chaque round), ce montant est payer grace au item qu'on manger

Donc, tous nos item sont transformer en $ et une partie est recupépré par le shopkeeper (si on a pas asser pour payer le shopkeeper, c'est game over)

puis on peut achter des upgrade proposer par le shopkeeper avec les $ qui nous reste

(pas sûr) le shopkeeper peut proposer de bet sur la prochaine round en donnant un objectif special, si cette objectif est atteint, on gagne des $ au prochain shop, si non sadge

Le nombre de $ demander a chaque round est exponentiel si jamais

aussi, plus un item as de décoration, plus il vos chère


Decroation sytem :
when spawn a item can have décoration, the more décrotaion is have, the more rare it is
certain item have base décroation (for example, idk, the speedy item have by default the speedy component) but it's not forbiden to have multiple of x decoration
rarity :
- 0-1 : common
- 2 : rare
- 3 : epic
- 4 : legendary
- more : godly



item décoration :
^ = not sure
- speed (or fire effect) : go faster for x time
- ^slow (or ice) : make you slower for x time
- ^bomb : spawn 4 bomb on eat (issac style)
- tp : make tp on a random spot
- (rare) lootbox : create a bunch of junk on eat
- (BAD) spike : hurt the player
- (BAD) poison : cut the player hp in half
- (BAD) curse : hurt the player but only if he stop eating for N sec, reset with every eated item
- ^(rare) hole : create a hole in which, the player is insta kill on eat (after 2 sec)
- speedy : make go a little faster, the more you eat, the faster you go
- (BAD) faite : make bad item effect lease rare
- ...
- ? : get a random effect (every effect as the same rarity in this case)

i think, bad effect should be rare but earn more money then any effect

Powerup :
- the more X item you have, the faster you go
- prevent X type of damage to damage the player
- ^prevent slow from slowing the player
- ^make the player able to eat bomb (bomb give a lot of money but hurt the player)
- being hit spawn as chance to spawn a godly item beside the player
- X item add a little time
- 1.15 * player hp
- block 1 damage pare round
- add a small invulerability when eating a item



bet :
- get N certain type of item
- get N certain item décoration
- dont get hit
- do not get a certain type of item
- find a lootbox
- passe the round with a max hazard level
