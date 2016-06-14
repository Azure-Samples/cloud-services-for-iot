# Building & Automating Hadoop Scripts

Hadoop is a powerful software framework and file system that can store a variety of different data sets of all shapes, sizes, speed, etc.  Generally speaking, it is the database software behind the industry term:  "Big Data".  Hadoop is much more than a database as it provides the platform for processing and ingesting data as well.  For this guide Hadoop is the persistent storage layer for more advanced data analysis of the CSP IoT-in-a-Box sensor data, specifically we will leverage the Hive component of the Hadoop ecosystem.  Hive is a data warehouse framework that was originally developed but Facebook, its purpose is for query, analysis, and most importantly data summarization.  Hive is typically where you'll find your traditional fact and dimension tables when you migrate your Enterprise Data Warehouse into the Hadoop/HDFS infrastructure.  This guide will walk you through parsing the sensor file/s, loading the sensor files into a table, and then grouping that data a series of columns in the CSV file and then persisting the aggregated output into an Azure Storage Table.  After you complete this guide you will have the following assets:

  - HiveQL Script to process and transform the raw sensor data into a Hive table format (rows and columns)
  - HiveQL table including summarized sensor data
  - Shutdown Script to destroy the compute portion of your HDInsight(Hadoop) cluster

### Prerequisites

+  PC with an RTM version of Windows 10 (the cluster is a Linux cluster, the UX is almost identical on Linux clients but this guidance is from the Windows 10 experience)
+  Provisioned CSP account and customer subscription in portal
+  Access to a CSP Subscription (this assumes you already have a co-admin login for a CSP subscription)
+  Deployed Azure CSP IoT-in-a-Box Solution #2 to the customer subscription
+  Some raw sensor data has been fed into Azure Storage account
+  Hadoop credential used in the ARM template of solution #2


### Version
0.0.1

### Accessing the Hive Query Interface

Once you have deployed the ARM template to the CSP subscription, you will need to login to your Hadoop cluster.  There are multiple ways you can use your Hadoop cluster but this guide will focus on the [Ambari] and [HiveQL] UI's for accesing data, executing scripts, and ultimately being the data warehouse component of the Hadoop ecosystem.  This entire exercise will be performed through the internet browser.  Login to the Azure portal, click on the HDInsight cluster you provisioned in the ARM template for solution #2.  You should see the following blades:

![HDInsight Default Portal View](https://0nm2va-ch3302.files.1drv.com/y3m3dIBOSPTslUk92WKPjdU1MsgIkCINBrN_-0gVsQw7ajogKmBK7BhFKTS2glhDo7oHYY7s1vTL3wOdF_MMhFx7-MukmWmkqL2JvKvD48LpIjDaonMWLIc_Mtyiy0GihiEshomN9yQR22lAFnn3KWBOkiB9XlfdJhVGUDDUE7S-WA?width=660&height=298&cropmode=none)

From the Azure portal you can navigate and launch the Ambari portal of your Hadoop cluster.  Do this by clicking on the **Ambari Views** button in the **Quick Links** menu on the HDInsight cluster blade.  This **Quick Links** menu is located in the middle section, click on **Ambari Views**:

![HDInsight Ambari QL Portal View](https://0nmxva-ch3302.files.1drv.com/y3mRoTd-oXSbIAsirZaPfZXYC9O5BnXkb7H6LFaDbZqcADNO7ITLKuF0YQc18Jb-0eH5WInI6HBB-xvbFoOv7o9LCPQVESMKaaXIcozj-b4ooA6UVTLbLP2_ZJmYQvfml4kgIXklg0EabyfgHK2-k_k54iPXz_DNsOT-NtpLvDaQsQ?width=660&height=298&cropmode=none)

When you click on the **Ambari Views** button your browser will launch a new tab, taking you to the Ambari home page.

![HDInsight Ambari View](https://0nmsva-ch3302.files.1drv.com/y3mCnSXk1gadRqrPcABk_PVNyUBx1ITHmUyLG2KmXfHBRuU22o6hY9xyc-iv2MRCVllwNUdaP1iqRYq-c1CqLG1FTCHNAuWETYpYCZh246PHzM6rOejM5QhrAr4M-oxkNywgn9whL8s7NZaLxTbgzdY3FexqSvlyjl9AYodQ2fvW0g?width=660&height=404&cropmode=none)

### Executing the Hive Query 

In the upper right-hand corner of the Ambari application is an icon that looks like a 2-dimensional rubix cube, ![HDInsight Ambari Hive View Menu](https://2xm1va-ch3302.files.1drv.com/y3mziVLe_48ErbPrzyMgnMuY3TrNJpWDjtYzs9QcGz2qzG2VINqsBbJsX6Dej5K1Ex99HvLWuRmGOUpcqmyzzuTUhiQ3I7bEUar6Wj9saZCtBM91SmlMRq0wN5AN5r7K74DyptrAU0X7Wiqu0X0sAxokQRa467uvbmB6IH0I4RWXKI?width=59&height=43&cropmode=none).  Click on this drop down menu and select **Hive Editor**.

![HDInsight Ambari View](https://2xm0va-ch3302.files.1drv.com/y3m1nAzyJpzW6piN9RKCagLkHvv8l7MIcQUuoUSGZcorWG08h1m6TDPNMAjJEK0LnQdKI9PP9E0mdTMQmv33Bj7Z7h1vEttOqhECPN-M4Wj7TNIR8nsUHX441TV6TXoWRK6a5qavOlSeNZlTkZPhOPRWSx_PKGyPAIOfTZeLh3kZOw?width=256&height=182&cropmode=none)

The **Hive View** is the GUI for Hadoop to browse objects and schema within the distributed file system.  We are now going to copy a HiveQL snippet/script from this guide into the editor.  Most often you will execute and/or open files but for this guidance we will copy the code into the editor.  The code is:

```sql
--HiveQL Editor
DROP TABLE IF EXISTS temp_sensor;
create table temp_sensor (col_value STRING);

LOAD DATA INPATH '/cspstoraget5fmowudia5fa/2016/04/15/21/2012733489_03c62024e2be4e059a88fe617b9ded92_1.csv' OVERWRITE INTO TABLE temp_sensor;

DROP TABLE IF EXISTS FactSensorHistory;
create table FactSensorHistory (Time DATETIME,SensorName STRING,Temperature FLOAT,Humidity FLOAT,City STRING,EventProcessedUtcTime DATETIME,PartitionId,EventEnqueuedUtcTime,IoTHub STRING);

insert overwrite table FactSensorHistory  
SELECT  
  regexp_extract(col_value, '^(?:([^,]*),?){1}', 1) Time,  
  regexp_extract(col_value, '^(?:([^,]*),?){2}', 1) SensorName,  
  regexp_extract(col_value, '^(?:([^,]*),?){3}', 1) Temperature,
  regexp_extract(col_value, '^(?:([^,]*),?){4}', 1) Humidity,
  regexp_extract(col_value, '^(?:([^,]*),?){5}', 1) City,
  regexp_extract(col_value, '^(?:([^,]*),?){6}', 1) EventProcessedUtcTime,
  regexp_extract(col_value, '^(?:([^,]*),?){7}', 1) IoTHub,
from temp_sensor;

SELECT * FROM FactSensorHistory;;

```

The **Hive View** should look like this once the code has been pasted into the editor:
![HDInsight Ambari View](https://2xm3va-ch3302.files.1drv.com/y3mCbRHEa9PUS1HHSWPt7KkrTGqXM5tCuyi4Lca9ZPyhvZwjUPCVeLJSeV_wS97Ro85JFyIt7bXeZZiiE6w3LWF6TrogYdTBxOauZhLe_L6GbFSlLmo_7wTv2p5_pu-21fzTHa4_9SEl-I7XMcKqwdWJJKQsn0-lnYbZWhx2dcCyV4?width=660&height=330&cropmode=none)

Click the green **Execute** button on the Query Editor page at the bottom left.

Once this script completes the results from the aggregate are then overwritten in the Hadoop table.  These results can then be accessed through the Azure Storage Connector and Hadoop Connector for Power BI.  Accessing these results will be covered in a later guide for Power BI.

### Next Steps and Automating Hadoop Script Execution

There are essentially 2 standard approaches for scheduling the Hadoop script:
  - PowerShell and Azure Automation
  - Azure Data Factory (control flow, execution) and Aure Automation (scheduler)

Azure Data Factory is a globally deployed PaaS data movement service hosted in the Azure public cloud.  It knows now boundaries when it comes to where it can connect to.  It is very Hadoop job and API friendly, which includes AzureML.  Azure Data Factory has native Hadoop/HDInsight-on-Demand capabilities where you literally only pay for the compute required to run the job, so you only would pay for the insert and group by from the example in this guide.  Of course you would also pay for the persistent storage but you would use that in an "on-demand" style of pricing and thinking.  


[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)


   [Ambari]: <https://ambari.apache.org/>
   [HiveQL]: <https://hive.apache.org/>
 
