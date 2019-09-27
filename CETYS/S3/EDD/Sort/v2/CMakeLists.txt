cmake_minimum_required(VERSION 3.5.1)

set(CMAKE_BUILD_TYPE Release)
set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
# set(EXECUTABLE_OUTPUT_PATH ${CMAKE_BINARY_DIR}/bin)
# set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall")

# message(${CMAKE_BINARY_DIR})
# message(${PROJECT_SOURCE_DIR})
# message(${PROJECT_BINARY_DIR})
# message(${CMAKE_CURRENT_SOURCE_DIR})
  
# Header directory
include_directories(include)

# Source directory
file(GLOB SOURCES "src/*.cpp")

# Generate shared library from the sources
add_library(stl SHARED ${SOURCES})

# Install the library in the project /lib location
install(TARGETS stl LIBRARY DESTINATION ${PROJECT_SOURCE_DIR}/lib )

enable_testing()

# Find boost test framework
# find_package(Boost COMPONENTS system filesystem unit_test_framework REQUIRED)
find_package(Boost COMPONENTS unit_test_framework REQUIRED)
add_definitions (-DBOOST_TEST_DYN_LINK)

include_directories(${Boost_INCLUDE_DIRS})

# The location of the test codes sources
file(GLOB TEST_SRCS RELATIVE ${CMAKE_CURRENT_SOURCE_DIR} test/*.cpp)

# Find test codes
foreach(testSrc ${TEST_SRCS})
    #Extract the filename without an extension (NAME_WE)
    get_filename_component(testName ${testSrc} NAME_WE)

    # Add compile target
    add_executable(${testName} ${testSrc})

    # Add dependency
    add_dependencies(${testName} stl)

    # link to Boost libraries AND your targets and dependencies
    target_link_libraries(${testName} ${Boost_LIBRARIES} ${CMAKE_CURRENT_SOURCE_DIR}/build/libstl.so )

    # Location of the test binaries 
    set_target_properties(${testName} PROPERTIES 
    RUNTIME_OUTPUT_DIRECTORY  ${CMAKE_CURRENT_SOURCE_DIR}/build/testBin)

    # Add the test to the collection of tests 
    # Notice the WORKING_DIRECTORY and COMMAND
    add_test(NAME ${testName} 
             WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}/build/testBin 
             COMMAND ${CMAKE_CURRENT_SOURCE_DIR}/build/testBin/${testName} )
endforeach(testSrc)
