# Consultation Type API — Reference for React Integration

Base route: `/api/ConsultationType`
Auth: JWT Bearer token required on every endpoint (`[Authorize]`), restricted to roles **Admin** and **Receptionist**.

Send the token on every request:
```
Authorization: Bearer <jwt_token>
```

---

## 1. Get All Consultation Types

**GET** `/api/ConsultationType/getAll`

**Request:** no params, no body.

**Response** `200 OK`
```json
[
  {
    "consultationTypeId": 1,
    "consultationType": "General Checkup"
  },
  {
    "consultationTypeId": 2,
    "consultationType": "Follow-up"
  }
]
```

---

## 2. Get Consultation Type By Id

**GET** `/api/ConsultationType/getById?consultationTypeId=1`

**Query params:** `consultationTypeId` (int, required)

**Response** `200 OK`
```json
{
  "consultationTypeId": 1,
  "consultationType": "General Checkup"
}
```

**Response** `404 Not Found` — when id doesn't exist (empty body).

---

## 3. Create Consultation Type

**POST** `/api/ConsultationType/create`

**Request body**
```json
{
  "consultationTypeId": 0,
  "consultationType": "Root Canal"
}
```
> `consultationTypeId` is ignored by the DB proc on insert — send `0`.

**Response** `200 OK`
```json
"Succefully Created !!"
```
*(plain string, note the API's actual spelling)*

---

## 4. Update Consultation Type

**POST** `/api/ConsultationType/update?consultationTypeId=1`

**Query params:** `consultationTypeId` (int, required — must match body's id or you get `400 Bad Request`)

**Request body**
```json
{
  "consultationTypeId": 1,
  "consultationType": "General Checkup - Updated"
}
```

**Response** `200 OK`
```json
"Succefully updated record !!"
```

**Response** `400 Bad Request` — when `consultationTypeId` query param ≠ `model.consultationTypeId`.

---

## 5. Delete Consultation Type

**POST** `/api/ConsultationType/delete?consultationTypeId=1`

**Query params:** `consultationTypeId` (int, required)

**Response** `200 OK`
```json
"Succefully deleted record !!"
```

---

## Common Error Responses

| Status | Cause |
|---|---|
| `401 Unauthorized` | Missing/expired/invalid JWT |
| `403 Forbidden` | Valid token but role isn't Admin/Receptionist |
| `400 Bad Request` | id mismatch on update |
| `404 Not Found` | getById with unknown id |
| `500 Internal Server Error` | Unhandled exception (logged server-side via `IExceptionLogger`) |

---

## React Integration (axios)

```js
// src/api/consultationTypeApi.js
import axios from "axios";

const API_BASE_URL = "https://<your-api-host>/api/ConsultationType";

const authHeader = () => ({
  headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
});

export const getAllConsultationTypes = () =>
  axios.get(`${API_BASE_URL}/getAll`, authHeader());

export const getConsultationTypeById = (consultationTypeId) =>
  axios.get(`${API_BASE_URL}/getById`, {
    ...authHeader(),
    params: { consultationTypeId },
  });

export const createConsultationType = (consultationType) =>
  axios.post(
    `${API_BASE_URL}/create`,
    { consultationTypeId: 0, consultationType },
    authHeader()
  );

export const updateConsultationType = (consultationTypeId, consultationType) =>
  axios.post(
    `${API_BASE_URL}/update`,
    { consultationTypeId, consultationType },
    { ...authHeader(), params: { consultationTypeId } }
  );

export const deleteConsultationType = (consultationTypeId) =>
  axios.post(`${API_BASE_URL}/delete`, null, {
    ...authHeader(),
    params: { consultationTypeId },
  });
```

### Example usage in a component

```jsx
import { useEffect, useState } from "react";
import {
  getAllConsultationTypes,
  createConsultationType,
  updateConsultationType,
  deleteConsultationType,
} from "../api/consultationTypeApi";

function ConsultationTypeList() {
  const [types, setTypes] = useState([]);

  const fetchAll = async () => {
    try {
      const res = await getAllConsultationTypes();
      setTypes(res.data);
    } catch (err) {
      console.error(err.response?.data || err.message);
    }
  };

  useEffect(() => {
    fetchAll();
  }, []);

  const handleCreate = async (name) => {
    await createConsultationType(name);
    fetchAll();
  };

  const handleUpdate = async (id, name) => {
    await updateConsultationType(id, name);
    fetchAll();
  };

  const handleDelete = async (id) => {
    await deleteConsultationType(id);
    fetchAll();
  };

  return (
    <ul>
      {types.map((t) => (
        <li key={t.consultationTypeId}>
          {t.consultationType}
          <button onClick={() => handleDelete(t.consultationTypeId)}>Delete</button>
        </li>
      ))}
    </ul>
  );
}

export default ConsultationTypeList;
```

### Notes for the frontend team
- Field names come back **camelCase** by default from ASP.NET's `System.Text.Json` (`consultationTypeId`, `consultationType`) unless the API has custom serializer settings — verify with a real response once deployed.
- `create`/`update`/`delete` return plain strings, not JSON objects — don't destructure `res.data.message`; use `res.data` directly, or treat `res.status === 200` as success.
- Attach a global axios interceptor to redirect to login on `401`, and show a "not authorized" message on `403`.
