cmake_minimum_required(VERSION 3.5.1)
project(perfeval)

set(CMAKE_BUILD_TYPE Release)
set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Build type
if (NOT CMAKE_BUILD_TYPE AND NOT CMAKE_CONFIGURATION_TYPES)
    message(STATUS "Setting build type to 'Debug' as none was specified.")
    set(CMAKE_BUILD_TYPE Debug CACHE STRING "Choose the type of build." FORCE)
    # Set the possible values of build type for cmake-gui
    set_property(CACHE CMAKE_BUILD_TYPE PROPERTY STRINGS "Debug" "Release")
endif ()

# Header location
include_directories( include )

# Source location
set(kmp_src src/kmp.cpp)


# Compilation flags
if (CMAKE_COMPILER_IS_GNUCC)
    set (CMAKE_C_FLAGS "-D_REETRANT -Wall -Wextra -pedantic -Wno-long-long")
    if (CMAKE_BUILD_TYPE STREQUAL "Debug")
        set (CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -ggdb -O0")
    elseif( CMAKE_BUILD_TYPE STREQUAL "Release" )
        set (CMAKE_C_FLAGS "${CMAKE_C_FLAGS} -DNDEBUG -O3 -fno-strict-aliasing")
    endif ()
endif (CMAKE_COMPILER_IS_GNUCC)

# Set client and server executable
add_executable(kmp ${kmp_src})
