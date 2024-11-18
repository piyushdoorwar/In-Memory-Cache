# Weather Forecast API with In-Memory Caching

This repository demonstrates a simple implementation of an ASP.NET Core Web API that fetches weather data from an external API and caches it using in-memory caching for improved performance.

---

## Features

- **Weather Data Fetching**: Retrieves current weather data for a given city code using an external weather API.
- **In-Memory Caching**: Stores weather data temporarily in memory to reduce redundant API calls and improve performance.
- **Automatic Expiration**: Cache entries expire after 2 minutes, ensuring data freshness.

---

## How It Works

1. **Caching Mechanism**:  
   When a request is made with a `cityCode`, the controller checks if the weather data for that city is already in the cache.
   - If **cached data** is found, it returns the cached response.
   - If **cached data** is not found, it fetches fresh data from the weather API, caches it, and then returns the response.

2. **External API Integration**:  
   The application uses the [WeatherAPI](https://www.weatherapi.com/) to fetch current weather information.

---

## API Endpoint

### `GET /WeatherForecast`
Fetches the weather data for a specified city code.

#### Query Parameters
- `cityCode` (required): The city zip code (US) to fetch the weather data for.  
  Example: `GET /WeatherForecast?cityCode=10012`

#### Response
- If data is found in the cache: Returns the cached weather data.
- If data is not in the cache: Fetches and returns fresh weather data.

---

## Technologies Used

- **ASP.NET Core**: Framework for building web APIs.
- **IMemoryCache**: In-memory caching for optimized data retrieval.
- **HttpClient**: To make HTTP requests to the external weather API.

---

## Setup and Running the Project

### Prerequisites
- .NET 6 or later installed.
- An API key from [WeatherAPI](https://www.weatherapi.com/) (replace `f7d132db2ce54be49ee81138241811` in the code with your API key).

### Steps
1. Clone the repository:

2. Replace the placeholder API key in `WeatherForecastController` with your API key:
   ```csharp
   var response = await _httpClient.GetStringAsync($"https://api.weatherapi.com/v1/current.json?q={cityCode}&key=YOUR_API_KEY");
   ```

3. Run the project:
   ```bash
   dotnet run
   ```

4. Test the endpoint using a tool like Postman or cURL:
   ```bash
   curl "http://localhost:5000/WeatherForecast?cityCode=10012"
   ```

---

## Example Usage

1. **First Request**:  
   Sends a request for weather data for a city code (e.g., `London`).  
   - **Result**: Fetches fresh data from the external API and caches it.

2. **Subsequent Request**:  
   Sends another request for the same city code within 2 minutes.  
   - **Result**: Returns the cached data.

3. **After Cache Expiration**:  
   Sends a request for the same city code after 2 minutes.  
   - **Result**: Fetches fresh data from the external API again.

---

## Notes

- The caching duration is currently set to **2 minutes**. You can adjust this in the `TimeSpan.FromMinutes(2)` line in the `WeatherForecastController`.
- This implementation uses in-memory caching, which is not shared across multiple instances of the application. For distributed scenarios, consider using a distributed cache like Redis.

---

## Future Improvements
- **Distributed Caching**: Replace in-memory cache with Redis or another distributed cache for scalability in multi-server environments.
- **Enhanced Error Handling**: Add proper error handling for external API failures or invalid inputs.
- **Logging**: Add structured logging for better debugging and monitoring.

---

Feel free to contribute by creating issues or submitting pull requests! 🎉
