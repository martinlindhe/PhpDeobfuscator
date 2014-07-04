<?php

$encoded = str_rot13('echo "hello\n";'); // rpub "uryyb\a";

eval(str_rot13('rpub "uryyb\a";'));

