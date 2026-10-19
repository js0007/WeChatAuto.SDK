cmake_minimum_required(VERSION 3.14)
project(wechatauto_client LANGUAGES CXX)

# Header-only interface library.
add_library(wechatauto_client INTERFACE)
target_include_directories(wechatauto_client INTERFACE
    $<BUILD_INTERFACE:${CMAKE_CURRENT_SOURCE_DIR}/include>
    $<INSTALL_INTERFACE:include>)
target_compile_features(wechatauto_client INTERFACE cxx_std_17)

# nlohmann/json (single header) is a required dependency of the generated header.
# Either install it system-wide / provide it on the include path, or use find_package.
find_package(nlohmann_json 3.10.0 QUIET)
if(nlohmann_json_FOUND)
    target_link_libraries(wechatauto_client INTERFACE nlohmann_json::nlohmann_json)
endif()

# Optional libcurl-backed default transport.
option(WECHATAUTO_ENABLE_CURL "Build with the libcurl-backed CurlTransport (defines WECHATAUTO_HAS_CURL)" OFF)
if(WECHATAUTO_ENABLE_CURL)
    find_package(CURL REQUIRED)
    target_link_libraries(wechatauto_client INTERFACE CURL::libcurl)
    target_compile_definitions(wechatauto_client INTERFACE WECHATAUTO_HAS_CURL)
endif()

# Unit tests (no live server; no extra test framework required).
option(WECHATAUTO_BUILD_TESTS "Build unit tests" ON)
if(WECHATAUTO_BUILD_TESTS)
    enable_testing()
    add_executable(test_client tests/test_client.cpp)
    target_link_libraries(test_client PRIVATE wechatauto_client)
    add_test(NAME test_client COMMAND test_client)
endif()
