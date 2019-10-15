#include <iostream>
#include <string>
#include <osrbtree.hpp>

Tree<int>* setup_tree()
{
    Tree<int>* t { new Tree<int>{ 10 } };
    int values[]{ 5, 4, 6, 15, 14, 16 };
    for(int i = 0; i < 7; i++)
        t->insert(values[i]);
    return t;
}

int main()
{
    Tree<int>* t = setup_tree();

    int res{ t->root->left->data };
    auto idk = res == 5;
    std::cout << idk << std::endl;
    delete t;
}
