using UnityEngine;//uses the unity engine library so you can use it's functions
using Pathfinding.Serialization.JsonFx; //make sure you include this using - links to the pathfinding file using it's namespace

public class Sketch : MonoBehaviour {//creates a class sketch
    public GameObject cube;//declares a gameobject which can be accessed from anywhere. this is a cube set in unity
    public GameObject sphere;//declares a gameobject which can be accessed from anywhere. this is a sphere set in unity
    public string _WebsiteURL = "http://infomgmt192.azurewebsites.net/tables/Mountain?zumo-api-version=2.0.0";//this a url to the table data Mountain

    void Start () {//this is what happens when your app runs
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        string jsonResponse = Request.GET(_WebsiteURL);

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Mountain[] mountains = JsonReader.Deserialize<Mountain[]>(jsonResponse);

        //----------------------
        //YOU WILL NEED TO DECLARE SOME VARIABLES HERE SIMILAR TO THE CREATIVE CODING TUTORIAL

        int i = 0;
        int totalCubes = mountains.Length;
        //float totalDistance = 2.9f;
        //----------------------

        //We can now loop through the array of objects and access each object individually
        foreach (Mountain mountain in mountains)
        {
            
            //Example of how to use the object
            Debug.Log("This mountains name is: " + mountain.MountainName);
            //----------------------
            //YOUR CODE TO INSTANTIATE NEW PREFABS GOES HERE
            float perc = i / (float)totalCubes;
            float sin = Mathf.Sin(perc * Mathf.PI / 2);

            float x = mountain.X;
            float y = mountain.Y;
            float z = mountain.Z;
            if(mountain.Symbol == "Sphere") {
                var newCube = (GameObject)Instantiate(sphere, new Vector3(x, y, z), Quaternion.identity);
                newCube.GetComponent<SphereScript>().SetSize(mountain.Size);
                newCube.GetComponent<SphereScript>().rotateSpeed = .2f + perc * 4.0f;
                newCube.transform.Find("New Text").GetComponent<TextMesh>().text = mountain.MountainName;//"Hullo Again";
                i++;
            }
            if (mountain.Symbol == "Cube") {
                var newCube = (GameObject)Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
                newCube.GetComponent<CubeScript>().SetSize(mountain.Size);
                newCube.GetComponent<CubeScript>().rotateSpeed = .2f + perc * 4.0f;
                newCube.transform.Find("New Text").GetComponent<TextMesh>().text = mountain.MountainName;//"Hullo Again";
                i++;
            }
            
            

            //----------------------
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
