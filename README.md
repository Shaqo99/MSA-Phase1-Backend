# MSA-Phase1-Backend

Databases and API MSA2020 Phase 1

http://msa-backend-phase-1.azurewebsites.net/index.html

**Microsoft learn module completion**

![](/images/createawebapi.PNG)


**Databases:** 

Student table has a one-to-many relationship with the Address table.
This is done by the address table having an addressID and studentID elements.
I used this method to easily show if a student has many addresses, if a student
has many addresses than they will have many addressID's. 


![](/images/tables.PNG)

**API:**

Image of all API Endpoints:

![](/images/apiendpointsv2.PNG)

Image of adding an address using studentID and addressID:
For adding an address, the studentID needs to be specified.

![](/images/addaddress.PNG)

Image of changing an address using studentID and addressID:
For updating an address, the studentID and addressID need to be specified.
The user will be unable to change an address for a student without the studentID.


![](/images/postaddress.PNG)
