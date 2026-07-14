# Implementation Migrasi Logic Controller ke CQRS

Dokumen ini menjadi panduan implementasi migrasi business logic dari `BusinessTripRequestsController` ke pola CQRS, mengikuti pola yang sudah ada pada:

- `Perdin.WebApi/Features/Auth/Login`
- `Perdin.WebApi/Features/Auth/Logout`

## Tujuan

1. Setiap endpoint API pada controller dipisahkan ke folder sesuai nama function.
2. DTO dipindahkan/ditaruh di folder endpoint masing-masing.
3. Setiap endpoint memiliki `Command/Query` dan `Handler` sendiri.

## Target Struktur Folder

Lokasi utama:

`Perdin.WebApi/Features/BusinessTripRequests`

Struktur yang diharapkan:

```text
Features/
  BusinessTripRequests/
    GetAll/
      GetAllBusinessTripRequestsRequest.cs
      GetAllBusinessTripRequestsResponse.cs
      GetAllBusinessTripRequestsQuery.cs
      GetAllBusinessTripRequestsQueryHandler.cs
    GetById/
      GetBusinessTripRequestByIdResponse.cs
      GetBusinessTripRequestByIdQuery.cs
      GetBusinessTripRequestByIdQueryHandler.cs
    Create/
      CreateBusinessTripRequestRequest.cs
      CreateBusinessTripRequestCommand.cs
      CreateBusinessTripRequestCommandHandler.cs
    Update/
      UpdateBusinessTripRequestRequest.cs
      UpdateBusinessTripRequestCommand.cs
      UpdateBusinessTripRequestCommandHandler.cs
    Delete/
      DeleteBusinessTripRequestCommand.cs
      DeleteBusinessTripRequestCommandHandler.cs
    Approval/
      ApprovalBusinessTripRequestRequest.cs
      ApprovalBusinessTripRequestCommand.cs
      ApprovalBusinessTripRequestCommandHandler.cs
```

> Catatan CQRS: endpoint `GET` gunakan `Query + QueryHandler`, endpoint write (`POST/PATCH/DELETE`) gunakan `Command + CommandHandler`.

## Mapping Endpoint -> Folder CQRS

| Endpoint Controller | Method | Folder | Objek CQRS |
|---|---|---|---|
| `GetAll` | `GET /api/business-trip-requests` | `GetAll` | Query |
| `GetById` | `GET /api/business-trip-requests/{id}` | `GetById` | Query |
| `Create` | `POST /api/business-trip-requests` | `Create` | Command |
| `Update` | `PATCH /api/business-trip-requests/{id}` | `Update` | Command |
| `Delete` | `DELETE /api/business-trip-requests/{id}` | `Delete` | Command |
| `Approval` | `PATCH /api/business-trip-requests/{id}/approval` | `Approval` | Command |

## Aturan Implementasi per Folder Endpoint

### 1) DTO per folder function

- Request DTO (input body/query parameter) diletakkan di folder endpoint terkait.
- Response DTO (output API) diletakkan di folder endpoint terkait.
- Hapus ketergantungan ke DTO lama bertahap setelah endpoint sudah sepenuhnya migrasi.

Contoh:
- `Create` menyimpan `CreateBusinessTripRequestRequest.cs`
- `Approval` menyimpan `ApprovalBusinessTripRequestRequest.cs`

### 2) Command/Query dan Handler per folder function

Setiap endpoint minimal memiliki:

- **Read endpoint**: `...Query.cs` + `...QueryHandler.cs`
- **Write endpoint**: `...Command.cs` + `...CommandHandler.cs`

Handler memuat seluruh business logic yang sebelumnya ada di controller:

- validasi aturan bisnis (contoh tanggal pulang < tanggal berangkat),
- query data EF Core,
- perhitungan (`DistanceHelper`, `PocketMoneyHelper`),
- transaksi database (`BeginTransactionAsync`, commit, rollback),
- status/approval flow.

### 3) Controller menjadi tipis (thin controller)

Controller hanya bertugas:

1. Validasi `ModelState`.
2. Mapping request HTTP -> command/query.
3. `await _mediator.Send(...)`.
4. Mapping hasil handler -> `ApiResponse`.

Business logic **tidak** lagi ditulis langsung di controller.

## Contoh Pola Controller Setelah Migrasi

```csharp
[HttpPost]
public async Task<IActionResult> Create([FromBody] CreateBusinessTripRequestRequest request)
{
    if (!ModelState.IsValid)
    {
        var errors = ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(e => e.ErrorMessage)
            .ToList();
        return BadRequest(ApiResponse<object>.ErrorResponse(string.Join("; ", errors)));
    }

    var command = new CreateBusinessTripRequestCommand
    {
        Request = request,
        CurrentUserId = GetCurrentUserId(),
        IsAdminOrSdm = IsAdminOrSdm()
    };

    var result = await _mediator.Send(command);
    return StatusCode(201, ApiResponse<object?>.SuccessResponse(result, "Pengajuan perjalanan dinas berhasil dibuat"));
}
```

## Urutan Migrasi yang Direkomendasikan

1. Buat folder `Features/BusinessTripRequests/{FunctionName}` untuk seluruh endpoint.
2. Pindahkan/buat DTO per folder endpoint.
3. Buat command/query + handler per endpoint.
4. Pindahkan business logic dari controller ke masing-masing handler.
5. Refactor controller agar hanya berisi orchestrasi HTTP + mediator.
6. Hapus kode logic lama di controller setelah endpoint terverifikasi berjalan.

## Checklist Verifikasi

- [ ] Semua endpoint `BusinessTripRequestsController` sudah menggunakan mediator.
- [ ] Tidak ada business logic utama tersisa di controller.
- [ ] DTO tiap endpoint berada di folder function masing-masing.
- [ ] Semua command/query handler ter-register via `AddMediatR(...)` (sudah ada di `Program.cs`).
- [ ] Response API tetap konsisten dengan format `ApiResponse`.

