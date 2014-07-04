<?php

$encoded = base64_encode(gzdeflate('echo "hello\n";')); //  S03OyFdQykjNycmPyVOyBgA=

echo eval(gzinflate(base64_decode('S03OyFdQykjNycmPyVOyBgA=')));
