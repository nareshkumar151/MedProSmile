# Doctor Consultant Fee API — Reference for React Integration

Base route: `/api/DoctorConsultantFee`
Auth: JWT Bearer token required on every endpoint (`[Authorize]`).

> **Admin-only note:** the backend currently annotates every action with
> `[Authorize(Roles = "Admin,Receptionist")]`, so a Receptionist token is
> technically accepted by the API today. Per product requirement this
> feature is **Admin-only**, so:
> - Only surface the "Doctor Consultation Fee" screens/menu inside the Admin
>   section of the React app (hide/guard the route for non-Admin roles).
> - Add a client-side role check (see `RequireAdmin` example below) so a
>   Receptionist-logged-in user can never reach the screen even by direct URL.
> - If you need this locked down server-side too, ask backend to change the
>   role list to `"Admin"` only on `DoctorConsultantFeeController`.

Send the token on every request:
```
Authorization: Bearer <jwt_token>
```

---

## Data shape — `DoctorConsultantFeeModel`

```ts
{
  consultantFeeId: number;   // PK, 0 on create
  hospitalId: number;        // required on create/update (not returned on GET yet)
  doctorId: number;          // FK -> Doctor
  fullName?: string;         // doctor's name — read-only, populated by the API on GET
  consultationTypeId: number;// FK -> ConsultationType
  consultationType?: string; // read-only, populated by the API on GET
  feeAmount: number;         // decimal
  isActive: boolean;
  createdOn?: string;        // ISO date, read-only
  createdBy?: number;
  updatedOn?: string;        // ISO date, read-only
  updatedBy?: number;
}
```

`fullName`, `consultationType`, `createdOn`, `updatedOn` are populated by the
stored procedures on **read** (joins against Doctor/ConsultationType) — don't
send them on create/update, the backend ignores them.

`hospitalId` is now **required** on create/update (as of 2026-08-24) — the
underlying stored procedures `usp_AddDoctorConsultantFee` and
`usp_UpdateDoctorConsultantFee` take it as a parameter. It is not currently
derived from the JWT — the client must send it in the body.

---

## 1. Get All Consultant Fees

**GET** `/api/DoctorConsultantFee/getAll`

**Request:** no params, no body.

**Response** `200 OK`
```json
[
  {
    "consultantFeeId": 1,
    "doctorId": 12,
    "fullName": "Dr. Ramesh Iyer",
    "consultationTypeId": 1,
    "consultationType": "General Checkup",
    "feeAmount": 500.00,
    "isActive": true,
    "createdOn": "2026-08-10T10:15:00",
    "createdBy": 3,
    "updatedOn": null,
    "updatedBy": null
  }
]
```

---

## 2. Get Consultant Fees By Doctor Id

**GET** `/api/DoctorConsultantFee/getByDoctorId?doctorId=12`

**Query params:** `doctorId` (int, required)

**Response** `200 OK`
```json
[
  {
    "consultantFeeId": 1,
    "doctorId": 12,
    "fullName": "Dr. Ramesh Iyer",
    "consultationTypeId": 1,
    "consultationType": "General Checkup",
    "feeAmount": 500.00,
    "isActive": true,
    "createdOn": "2026-08-10T10:15:00",
    "createdBy": 3,
    "updatedOn": null,
    "updatedBy": null
  }
]
```
Returns `[]` (empty array) if the doctor has no fee records — not a 404.

---

## 3. Create Consultant Fee

**POST** `/api/DoctorConsultantFee/create`

**Request body**
```json
{
  "hospitalId": 1,
  "doctorId": 12,
  "consultationTypeId": 1,
  "feeAmount": 500.00,
  "createdBy": 3
}
```
> Only `hospitalId`, `doctorId`, `consultationTypeId`, `feeAmount`, `createdBy`
> are read by the backend on create — `consultantFeeId` is ignored, send `0`
> or omit.

**Response** `200 OK`
```json
{
  "consultantFeeId": 7,
  "message": "Succefully Created !!"
}
```
*(note the API's actual spelling of "Succefully")*

---

## 4. Update Consultant Fee

**POST** `/api/DoctorConsultantFee/update?consultantFeeId=7`

**Query params:** `consultantFeeId` (int, required — must match body's
`consultantFeeId` or you get `400 Bad Request`)

**Request body**
```json
{
  "consultantFeeId": 7,
  "hospitalId": 1,
  "doctorId": 12,
  "consultationTypeId": 1,
  "feeAmount": 650.00,
  "isActive": true,
  "updatedBy": 3
}
```

**Response** `200 OK`
```json
"Succefully updated record !!"
```

**Response** `400 Bad Request` — when `consultantFeeId` query param ≠
`model.consultantFeeId`.

**Response** `404 Not Found` — when no row matched (0 rows affected).

---

## 5. Delete Consultant Fee

**POST** `/api/DoctorConsultantFee/delete?consultantFeeId=7&updatedBy=3`

**Query params:**
- `consultantFeeId` (int, required)
- `updatedBy` (int, optional — pass the logged-in admin's user id for the
  audit trail)

**Response** `200 OK`
```json
"Succefully deleted record !!"
```

**Response** `404 Not Found` — when no row matched (0 rows affected).

> The controller/repository code doesn't reveal whether the stored procedure
> `usp_DeleteDoctorConsultantFee` hard-deletes the row or just sets
> `IsActive = 0` (it does accept `updatedBy`, which is typical of a
> soft-delete audit trail). Confirm with backend/DB before assuming either
> behavior in the UI.

---

## Common Error Responses

| Status | Cause |
|---|---|
| `401 Unauthorized` | Missing/expired/invalid JWT |
| `403 Forbidden` | Valid token but role isn't Admin (Receptionist is still accepted by the backend today — see note above) |
| `400 Bad Request` | `consultantFeeId` mismatch on update |
| `404 Not Found` | update/delete on unknown id |
| `500 Internal Server Error` | Unhandled exception (logged server-side via `IExceptionLogger`) |

---

## React Integration (axios)

```js
// src/api/doctorConsultantFeeApi.js
import axios from "axios";

const API_BASE_URL = "https://<your-api-host>/api/DoctorConsultantFee";

const authHeader = () => ({
  headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
});

export const getAllConsultantFees = () =>
  axios.get(`${API_BASE_URL}/getAll`, authHeader());

export const getConsultantFeesByDoctorId = (doctorId) =>
  axios.get(`${API_BASE_URL}/getByDoctorId`, {
    ...authHeader(),
    params: { doctorId },
  });

export const createConsultantFee = ({
  hospitalId,
  doctorId,
  consultationTypeId,
  feeAmount,
  createdBy,
}) =>
  axios.post(
    `${API_BASE_URL}/create`,
    { hospitalId, doctorId, consultationTypeId, feeAmount, createdBy },
    authHeader()
  );

export const updateConsultantFee = ({
  consultantFeeId,
  hospitalId,
  doctorId,
  consultationTypeId,
  feeAmount,
  isActive,
  updatedBy,
}) =>
  axios.post(
    `${API_BASE_URL}/update`,
    { consultantFeeId, hospitalId, doctorId, consultationTypeId, feeAmount, isActive, updatedBy },
    { ...authHeader(), params: { consultantFeeId } }
  );

export const deleteConsultantFee = (consultantFeeId, updatedBy) =>
  axios.post(`${API_BASE_URL}/delete`, null, {
    ...authHeader(),
    params: { consultantFeeId, updatedBy },
  });
```

### Client-side admin gate

```jsx
// src/components/RequireAdmin.jsx
import { Navigate } from "react-router-dom";
import { getUserRole } from "../auth/session"; // however the app reads the JWT role today

function RequireAdmin({ children }) {
  const role = getUserRole();
  if (role !== "Admin") {
    return <Navigate to="/unauthorized" replace />;
  }
  return children;
}

export default RequireAdmin;
```

```jsx
// wherever routes are declared
<Route
  path="/admin/doctor-consultant-fees"
  element={
    <RequireAdmin>
      <DoctorConsultantFeeList />
    </RequireAdmin>
  }
/>
```

### Example usage in a component

```jsx
import { useEffect, useState } from "react";
import {
  getAllConsultantFees,
  createConsultantFee,
  updateConsultantFee,
  deleteConsultantFee,
} from "../api/doctorConsultantFeeApi";

function DoctorConsultantFeeList({ currentUserId }) {
  const [fees, setFees] = useState([]);

  const fetchAll = async () => {
    try {
      const res = await getAllConsultantFees();
      setFees(res.data);
    } catch (err) {
      console.error(err.response?.data || err.message);
    }
  };

  useEffect(() => {
    fetchAll();
  }, []);

  const handleCreate = async (doctorId, consultationTypeId, feeAmount) => {
    await createConsultantFee({
      doctorId,
      consultationTypeId,
      feeAmount,
      createdBy: currentUserId,
    });
    fetchAll();
  };

  const handleUpdate = async (fee, newFeeAmount) => {
    await updateConsultantFee({
      ...fee,
      feeAmount: newFeeAmount,
      updatedBy: currentUserId,
    });
    fetchAll();
  };

  const handleDelete = async (consultantFeeId) => {
    await deleteConsultantFee(consultantFeeId, currentUserId);
    fetchAll();
  };

  return (
    <ul>
      {fees.map((f) => (
        <li key={f.consultantFeeId}>
          {f.fullName} — {f.consultationType} — ₹{f.feeAmount}
          <button onClick={() => handleDelete(f.consultantFeeId)}>Delete</button>
        </li>
      ))}
    </ul>
  );
}

export default DoctorConsultantFeeList;
```

### Notes for the frontend team
- Field names come back **camelCase** from ASP.NET's `System.Text.Json`
  (`consultantFeeId`, `doctorId`, `feeAmount`, …) — verify against a real
  response once deployed.
- `create` returns a JSON object (`{ consultantFeeId, message }`); `update`
  and `delete` return **plain strings**, not JSON objects — don't
  destructure `res.data.message` for those, use `res.data` directly or check
  `res.status === 200`.
- Attach a global axios interceptor to redirect to login on `401`, and show
  a "not authorized" message on `403`.
- Whether `delete` hard-removes the row or soft-deletes it (`isActive: false`)
  is determined by the DB stored procedure, which isn't in this repo —
  confirm with backend. If it's a soft delete, deactivated rows may still
  come back from `getByDoctorId`/`getAll`; filter on `isActive` client-side
  if the UI should hide them.
