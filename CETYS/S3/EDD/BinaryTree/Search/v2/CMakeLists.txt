cmake_minimum_required(VERSION 3.0)

project(Trees VERSION 0.1 LANGUAGES CXX)
set(CMAKE_BUILD_TYPE Debug)

include_directories(include)

file(GLOB CPPS "src/*.cpp")
add_library(helper SHARED ${CPPS})

find_package(Boost COMPONENTS unit_test_framework REQUIRED)
include_directories(${Boost_INCLUDE_DIRS})
add_definitions(-DBOOST_TEST_DYN_LINK)

enable_testing()
file(GLOB TEST_CPPS "test/*.cpp")

foreach(SINGLE_TEST ${TEST_CPPS})
    get_filename_component(TEST_NAME ${SINGLE_TEST} NAME_WE)
    add_executable(${TEST_NAME} ${SINGLE_TEST})
    add_dependencies(${TEST_NAME} helper)

    target_link_libraries(
        ${TEST_NAME} 
        ${Boost_LIBRARIES} 
        ${CMAKE_CURRENT_SOURCE_DIR}/build/libhelper.so
    )

    set_target_properties(
        ${TEST_NAME} 
        PROPERTIES RUNTIME_OUTPUT_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}/build/outest
    )

    add_test(
        NAME ${TEST_NAME} 
        WORKING_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}/build/outest 
        COMMAND ${CMAKE_CURRENT_SOURCE_DIR}/build/outest/${TEST_NAME}
    )
endforeach(SINGLE_TEST)

add_executable(trees src/main.cpp)
