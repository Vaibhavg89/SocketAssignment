# \# 🔌 Socket Programming Assignment — Nerve Solutions

# 

# This project demonstrates a simple \*\*TCP client-server communication\*\* system in \*\*.NET 8\*\* with \*\*AES encryption/decryption\*\*.  

# It was developed as part of a recruitment assignment for \*\*Nerve Solutions\*\*.

# 

# ---

# 

# \## 📁 Project Structure

# 

# The solution contains \*\*two console applications\*\*:

# 

SocketAssignment/

│

├── Server/ → TCP Server app

│ ├── Program.cs

│ └── EncryptionHelper.cs

│

└── Client/ → TCP Client app

├── Program.cs

└── EncryptionHelper.cs







Both projects use the same `EncryptionHelper` class for \*\*symmetric AES encryption\*\* to securely exchange data.



---



\## ⚙️ Features



\### 🖥️ Server

\- Listens for incoming TCP client connections (default: port `5000`)

\- Decrypts incoming client messages

\- Parses requests in the format `SetX-SubKey`

\- Looks up values from predefined sets (SetA–SetE)

\- Sends back current timestamp \*\*n times\*\* (based on value from set)

\- Encrypts responses before sending to the client



\### 💻 Client

\- Connects to the server via TCP

\- Sends an \*\*encrypted message\*\* (e.g. `SetA-Two`)

\- Decrypts and displays server responses (timestamps)

\- Gracefully handles invalid or empty responses



---



\## 🔐 Message Format



| Example Input | Meaning |

|----------------|----------|

| `SetA-One` | From SetA, pick value `1` → send 1 timestamp |

| `SetA-Two` | From SetA, pick value `2` → send 2 timestamps |

| `SetB-Four` | From SetB, pick value `4` → send 4 timestamps |

| Invalid Input | Server replies with `"EMPTY"` |



---



\## 🧩 Example Flow



\### 1️⃣ Run the Server

```bash

dotnet run --project Server



Output:

=== TCP SERVER STARTED ===

Enter port to listen on (default 5000):

Server listening on port 5000...



2️⃣ Run the Client

dotnet run --project Client



Output:

=== TCP CLIENT STARTED ===

Enter server IP (default 127.0.0.1):

Enter server port (default 5000):

Enter message (e.g. SetA-Two): SetA-Two

Received: 31-10-2025 12:41:22

Received: 31-10-2025 12:41:23

Client finished.





🔒 Encryption Details



A simple AES-based symmetric encryption is used to secure communication between client and server.

Both use the same secret key and IV (defined in EncryptionHelper.cs).



## 🧠 Tech Stack



- **.NET 8.0



- **C# (Async/Await)



- **TCP/IP Sockets



- **AES Encryption





🚀 How to Run



1. Clone this repository



git clone https://github.com/Vaibhavg89/socketassignment.git



2. Open the solution in Visual Studio 2022 or newer.



3. Set Server as startup project → Run (Start Debugging)



4. Set Client as startup project → Run in a new console window



Follow the prompts and observe encrypted communication!

