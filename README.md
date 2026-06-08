# MCR-Project



## TODO
- **[ X ]** item system
  - **[ X ]** spawn item
  - **[ X ]** item decoration
  - **[ X ]** wrapper
  - **[ X ]** item player hook
  - **[ X ]** item sell

- **[ X ]** player
  - **[ X ]** player base movement
    - **[ X ]** eat
    - **[ X ]** move
  - **[ X ]** hp system
  - **[ X ]** take damage

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
    - **[ X ]** move
    - **[ X ]** death
    - **[ X ]** spawn
    - **[ X ]** dash
  - **[ X ]** shopkeeper
    - **[ X ]** ask money
    - **[ X ]** selling time
    - **[ X ]** bet
    - **[ X ]** gameover
  - [ _ ] hazard
    - [ _ ] hit box
  - **[ X ]** item
    - **[ X ]** spawn


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
- x speed (or fire effect) : go faster for x time
- x tp : make tp on a random spot
- x (rare) lootbox : create a bunch of junk on eat
- x speedy : make go a little faster, the more you eat, the faster you go
- x heal : recover N hp
- x invulerability : prevent dega
- x greedy : make every item eat price x 2, stop if nothing is eated for 0.5sec

- (Discard)(BAD) faite : make bad item effect lease rare
- x (BAD) spike : hurt the player
- x (BAD) poison : cut the player hp in half
- x (BAD) curse : hurt the player but only if he stop eating for N sec, reset with every eated item
- x random : get a random effect (every effect as the same rarity in this case)

i think, bad effect should be rare but earn more money then any effect

Powerup :
- price : moyen     | prevent X type of damage to damage the player
- price : pas chère | being hit spawn as chance to spawn a godly item beside the player
- price : moyen     | X item add a little time
- price : chère     | make every item have a N decorator
- price : moyen     | 1.15 * player hp
- price : pas chère | block 1 damage pare round
- price : chère     | add a small invulerability when eating a item
- price : pas chère | make N decorator more likly




bet :
- get N certain type of item
- dont get hit
- get a specific decorator
- passe the round with a max hazard level
-
