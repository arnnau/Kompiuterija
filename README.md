# Documentation

## Shops

### Get list

```http
GET /shop/all
```
Returns all shops in the database

### Get single

```http
GET /shop/***REMOVED***id***REMOVED***
```
Returns specified shop

### Get hierarchical

```http
GET /shop/***REMOVED***id***REMOVED***/computers
```
Returns all computers belonging to specified shop

### Post

```http
POST /shop/insert
```
Inserts new item to database. Example body:
```
***REMOVED***
  "id": 0,
  "address": "string",
  "city": "string"
***REMOVED***
```

### Put

```http
PUT /shop/update
```
Updates item in the database. Example body:
```
***REMOVED***
  "id": 0,
  "address": "string",
  "city": "string"
***REMOVED***
```

### Delete

```http
DELETE /shop/***REMOVED***id***REMOVED***
```
Deletes specified shop

## Computers

### Get list

```http
GET /computer/all
```
Returns all computers in the database

### Get single

```http
GET /computer/***REMOVED***id***REMOVED***
```
Returns specified computer

### Post

```http
POST /computer/insert
```
Inserts new item to database. Example body:
```
***REMOVED***
  "id": 0,
  "userId": 0,
  "shopId": 0,
  "name": "string",
  "registered": "2022-10-06T18:51:32.189Z"
***REMOVED***
```

### Put

```http
PUT /computer/update
```
Updates item in the database. Example body:
```
***REMOVED***
  "id": 0,
  "userId": 0,
  "shopId": 0,
  "name": "string",
  "registered": "2022-10-06T18:51:32.189Z"
***REMOVED***
```

### Delete

```http
DELETE /computer/***REMOVED***id***REMOVED***
```
Deletes specified computer

## Parts

### Get list

```http
GET /part/all
```
Returns all parts in the database

### Get single

```http
GET /part/***REMOVED***id***REMOVED***
```
Returns specified part

### Post

```http
POST /part/insert
```
Inserts new item to database. Example body:
```
***REMOVED***
  "id": 0,
  "computerId": 0,
  "type": "string",
  "name": "string"
***REMOVED***
```

### Put

```http
PUT /part/update
```
Updates item in the database. Example body:
```
***REMOVED***
  "id": 0,
  "computerId": 0,
  "type": "string",
  "name": "string"
***REMOVED***
```

### Delete

```http
DELETE /part/***REMOVED***id***REMOVED***
```
Deletes specified part
