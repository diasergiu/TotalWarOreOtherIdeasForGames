# Applicati

I made this applicaiton to store the ideas about a possible future project of mine where i made a tactical total war like game. Curently the application
stors what is suppose to be the units and the factions that thowse units belong to. Curently the application makes the CRUD opperations with with the 
curent models: Factions, SoldierModels(witch are the individual soldiers that are in a formation), Items( witch are the weapons used by individual soldiers)
,Traits (witch are specific traits and abylities that specific formations have), Horses( witch a specific formation micht have if it is mounded), barding
(witch some horses are equipt with to protect the horses), and formations ( with teigh together all the tables). 

The ralationship between the table are mostly many to many: SoldierModels with items, Trait with Formations, Formation with Factions.and we have 
some relations of type one to many:Horse with barding, Formation with horse,Formation with SoldierModel. The deletion is mostly done on cascade as
the joint table relations ore the ocasion where we delete a SoldierModel and we have a formation without soldiers have no sence and are unnasecery.

The applicaiton is also made so that you can configure relations the relationship with other tables even if they are in a many to many ore 
one to many relation in a create ore update configurationn

The project have also a set of functionalities such as paginations.(uninplemented) search on model for diferent criteria, a user base in the database
that can logIn and pass the security with an encripted password, encription to pictures.

(probably a cut)
The applicaiton is suppose to fallow a clean architecture and is suppose to be implemented in a TDD(test driven development) way. 

#Technologies:

this is a wep api application made in .net 5 with entity framework core 5.0.17 that connects to an sql server 2017 database 