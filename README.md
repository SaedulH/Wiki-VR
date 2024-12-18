# WikiVR
A Virtual Reality Wikipedia Explorer. 
To access the graph in the application, Neo4j Desktop is required to reproduce the database because a remote server has not been set up,  so a local connection must be made. Install the latest version of Neo4j desktop (version 1.4.15) from here: https://neo4j.com/download-center/#desktop
In the supplementary folder, locate the dump file named “Wiki-VR-Neo4j.dump”, This contains all the graph data needed to clone the database.
Once installed, Follow the step-by-step guide below:
1.	Create a new project from the “new” dropdown menu near the top left
2.	Open the project’s file location by clicking “Reveal files in File explorer” on the right-hand-side
3.	Locate the provided dump file and drag and drop it into the project’s file location 
4. The dump file should now appear in Neo4j, click on its options button, and select “Create new DBMS from dump” 
5. The name can be anything you’d like (this does not affect the setup so can be left as is), but the password must be “wiki” – no capitals - and using version 4.4.5.

6. Once the dump file has successfully loaded the database, click “Start” to run it, you can check if the process was properly executed by opening the graph in the Neo4j browser. Values should be identical to the values shown above.
The database is now ready, you can close the Neo4j browser but keep the database running on Neo4k Desktop while using the application. 
If using an Oculus standalone HMD, connect either by Airlink, virtual desktop or cable. To run the application, ensure that SteamVR is installed from Steam;  https://store.steampowered.com/app/250820/SteamVR/, upon opening the WikiVR application, SteamVR should run automatically. Tethered headsets will also run the application using SteamVR. 
