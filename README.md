# Unity-ARFoundation-echoAR-demo-Per-Capita-GDP-Comparison
Simple GDP Per Capita Comparison demo with Unity, AR Foundation, and echoAR.

## Setup
* Clone the [Unity+ARFoundation with echoAR](https://github.com/echoARxyz/Unity-ARFoundation-echoAR-example) example code.
* Follow the instructions on our [doumention page](https://docs.echoar.xyz/unity/adding-ar-capabilities) to [set your API key](https://docs.echoar.xyz/unity/adding-ar-capabilities#3-set-you-api-key).
* [Add the 3D assets](https://docs.echoar.xyz/quickstart/add-a-3d-model) from the [assets](https://github.com/ryanrx/Unity-ARFoundation-echoAR-demo-Per-Capita-GDP-Comparison/tree/master/assets) folder to the console.
* [Add the metadata](https://docs.echoar.xyz/web-console/manage-pages/data-page/how-to-add-data#adding-metadata) listed in the the [metadata](https://github.com/ryanrx/Unity-ARFoundation-echoAR-demo-Per-Capita-GDP-Comparison/blob/master/metadata.csv) file to the hologram.
* Overwrite the existing _echoAR/CustomBehaviour.cs_ script with the new [_CustomBehaviour.cs_](https://github.com/ryanrx/Unity-ARFoundation-echoAR-demo-Per-Capita-GDP-Comparison/blob/master/CustomBehaviour.cs) file. Feel free to add more countries to the list in the script.

## Run
* [Build and run the AR application](https://docs.echoar.xyz/unity/adding-ar-capabilities#4-build-and-run-the-ar-application).

## Real-time updates
Update the value of the metadata associated with the sphere model and notice that the size of spheres in the AR scene changes automatically. Adding/Deleting countries also updates automatically.

## Screenshots
![Unity scene screenshot](/images/screenshot1.png)
![echoAR console screenshot](/images/screenshot2.png)
![echoAR console screenshot](/images/screenshot3.png)
