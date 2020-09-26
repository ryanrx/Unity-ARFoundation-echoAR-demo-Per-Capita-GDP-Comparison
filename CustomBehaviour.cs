/**************************************************************************
* Copyright (C) echoAR, Inc. 2018-2020.                                   *
* echoAR, Inc. proprietary and confidential.                              *
*                                                                         *
* Use subject to the terms of the Terms of Service available at           *
* https://www.echoar.xyz/terms, or another agreement                      *
* between echoAR, Inc. and you, your company or other organization.       *
***************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBehaviour : MonoBehaviour
{
    [HideInInspector]
    public Entry entry;

    /// <summary>
    /// EXAMPLE BEHAVIOUR
    /// Queries the database and names the object based on the result.
    /// </summary>
    ///

    // list of category names to look for
    private string[] categNames =
    {
        "USA",
        "China",
        "Israel",
        "Switzerland",
        "Vietnam",
        "Cambodia",
        "Japan"
    };

    // data structure to store the stat, model and text
    public class Data
    {
        public int stat;
        public GameObject text;
        public GameObject categ;

        public Data(int stat, GameObject categ, GameObject text)
        {
            this.stat = stat;
            this.categ = categ;
            this.text = text;
        }
    }

    // Dictionary to store current objects
    private Dictionary<string, Data> dataMap = new Dictionary<string, Data>();

    // number of models instantiated
    private int count = 0;

    // Maximum value for metadata
    private static float maxVal = 200000f;

    // Max value of length, width, height
    private float maxSide = (float)System.Math.Pow(maxVal, (1.0 / 3));



    // Use this for initialization
    void Start()
    {
        // Add RemoteTransformations script to object and set its entry
        //this.gameObject.AddComponent<RemoteTransformations>().entry = entry;

        // Qurey additional data to get the name
        string value = "";
        if (entry.getAdditionalData() != null && entry.getAdditionalData().TryGetValue("name", out value))
        {
            // Set name
            this.gameObject.name = value;
        }

        // hide the original game object
        if (this.gameObject.name.Equals("sphere"))
        {
            this.gameObject.transform.localScale = new Vector3(0, 0, 0);
        }
        //Debug.Log(maxSide);
    }

    // Update is called once per frame
    void Update()
    {
        // code only run by original game object
        if (this.gameObject.name.Equals("sphere"))
        {
            if (entry.getAdditionalData() != null)
            {
                // iterate through all the categories
                foreach (string category in categNames)
                {
                    // query additional data to get category stats
                    string statString = "";
                    if (entry.getAdditionalData().TryGetValue(category, out statString))
                    {
                        // parse the stat
                        int stat = int.Parse(statString);

                        Data data;
                        // rescale the object if it already exists and stat changes
                        if (dataMap.TryGetValue(category, out data))
                        {
                            if (data.stat != stat)
                            {
                                float side = (float)System.Math.Pow(stat, (1.0 / 3));
                                float scale = side / maxSide / 10f;

                                float oldSide = (float)System.Math.Pow(data.stat, (1.0 / 3));
                                float oldScale = oldSide / maxSide / 10f;
                                data.categ.transform.localScale = new Vector3(scale, scale, scale);
                                data.categ.transform.position += new Vector3(9f * (scale - oldScale), 0, -30f * (scale * scale - oldScale * oldScale));
                                data.stat = stat;
                            }
                        }
                        // instantiate new object if it does not exist
                        else
                        {
                            // clone original game object
                            this.gameObject.name = "Sphere " + category;
                            GameObject categ = Instantiate(this.gameObject);
                            this.gameObject.name = "sphere";

                            // calculate the scale of length, width, height
                            float side = (float)System.Math.Pow(stat, (1.0 / 3));

                            // scale and position the new game object
                            float scale = side / maxSide / 10f;
                            float xdisp = 9f * scale;
                            float zdisp = -30f * scale * scale;
                            categ.transform.localScale = new Vector3(scale, scale, scale);
                            categ.transform.position = new Vector3(1f + 0.3f * count + xdisp, 0, zdisp);
                            count++;
                            //Debug.Log(count);

                            // set a common parent for model and text
                            GameObject ballBase = new GameObject("ballBase" + category);
                            ballBase.AddComponent<MeshFilter>();
                            ballBase.name = category;
                            categ.transform.parent = ballBase.transform;

                            // make and position the text object
                            GameObject text = new GameObject();
                            TextMesh t = text.AddComponent<TextMesh>();
                            t.text = category;
                            t.fontSize = 100;
                            text.name = "Text " + category;
                            text.transform.position = categ.transform.position - new Vector3(xdisp, 0, zdisp);
                            text.transform.localScale = new Vector3(0.005f, 0.005f, 0.005f);
                            text.transform.eulerAngles = new Vector3(0, 0, 90);

                            text.transform.parent = ballBase.transform;

                            // add the new stat and objects to the dictionary
                            data = new Data(stat, categ, text);

                            dataMap.Add(category, data);

                        }
                    }
                    // delete parent object if the category no longer exists 
                    else
                    {
                        dataMap.Remove(category);
                        Destroy(GameObject.Find(category));
                    }
                }
            }
        }
    }


}