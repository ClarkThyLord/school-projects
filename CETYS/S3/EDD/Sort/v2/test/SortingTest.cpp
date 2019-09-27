#define BOOST_TEST_MODULE "Sorting test"
#define BOOST_TEST_MAIN

#include <boost/test/unit_test.hpp>
#include <boost/mpl/list.hpp>
#include <iostream>
#include <bitset>
#include "Sort.hpp"

using namespace std;


BOOST_AUTO_TEST_SUITE(LLTEST)


typedef boost::mpl::list<int,long> test_types;

// Define a sorting criterion
template<typename T>
bool criterio(T& i,T& j) { return (i<j); }


// Placeholder function that creates a static unshufled sequence
template<typename T>
std::vector<T> create_static_sequence() {

    std::vector<T> elements;
        
    // Creation of an anorder set of elements
    elements.push_back(100);
    elements.push_back(10);
    elements.push_back(80);
    elements.push_back(30);
    elements.push_back(60);
    elements.push_back(50);
    elements.push_back(40);
    elements.push_back(70);
    elements.push_back(20);
    elements.push_back(90);
    
    //Not very elegant! A copy is created. 
    return elements;
}

// Covert values to a string
template<typename T>
std::string to_string(const std::vector<T> &elements){
    std::string str{};
    for(T e : elements) str += std::to_string(e) + ", ";
    return str.substr(0,str.length()-2);
}


BOOST_AUTO_TEST_CASE_TEMPLATE( sorting_test, T, test_types )
{
    bitset<2> state;

    // Create a static sequence of unordered elements
    std::vector<T> elements = create_static_sequence<T>();

    // Test cases are reset to 0. None have pass yet!
    state.reset();

    // Print out the contents of the vector
    std::cout << "First unordered sequence : " <<  to_string(elements) << std::endl;

    // Sort the complete set of elements
    ordenamiento<T>::ordena(elements.begin(), elements.end());
    
    // Print the sorted vector
    std::cout << "First ordered sequence : " << to_string(elements) << std::endl;

    // Evaluate test sequence 1
    std::string sequence_1{"10, 20, 30, 40, 50, 60, 70, 80, 90, 100"};
    if(to_string(elements) == sequence_1)
        state.set(0);

    // Create the static sequence of unordered numbers again
    elements = create_static_sequence<T>();

    std::cout << "Second unordered sequence : " << to_string(elements) << std::endl;

    // Sort only six elements using the custom made comparison criterion
    ordenamiento<T>::ordena(elements.begin()+4, elements.end(), &criterio);

    std::cout << "Second partially ordered sequence : " << to_string(elements) << std::endl;
    
    // Evaluate test case 2
    std::string sequence_2{"100, 10, 80, 30, 20, 40, 50, 60, 70, 90"};
    if( to_string(elements) == sequence_2)
        state.set(1);

    BOOST_CHECK_EQUAL(state.all(),true);  
}

BOOST_AUTO_TEST_SUITE_END()
